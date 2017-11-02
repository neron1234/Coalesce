﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IntelliTect.Coalesce.CodeGeneration.Templating;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IntelliTect.Coalesce.CodeGeneration.Generation
{
    public class CompositeGeneratorServices
    {
        public CompositeGeneratorServices(
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory)
        {
            ServiceProvider = serviceProvider;
            LoggerFactory = loggerFactory;
        }

        public IServiceProvider ServiceProvider { get; }
        public ILoggerFactory LoggerFactory { get; }
    }

    public abstract class CompositeGenerator<TModel> : ICompositeGenerator<TModel>
    {
        protected ILogger Logger { get; }
        private readonly CompositeGeneratorServices _services;

        public string OutputPath { get; set; }
        public TModel Model { get; set; }

        public List<ICleaner> Cleaners { get; protected set; } = new List<ICleaner>();

        public CompositeGenerator(CompositeGeneratorServices services)
        {
            this._services = services;
            Logger = services.LoggerFactory.CreateLogger(GetType().Name);
        }

        protected TGenerator Generator<TGenerator>()
            where TGenerator : IGenerator
        {
            var generator = ActivatorUtilities.CreateInstance<TGenerator>(_services.ServiceProvider);
            generator.OutputPath = OutputPath;
            return generator;
        }

        protected TCleaner Cleaner<TCleaner>()
            where TCleaner : ICleaner
        {
            var cleaner = ActivatorUtilities.CreateInstance<TCleaner>(_services.ServiceProvider);
            cleaner.Owner = this;
            cleaner.TargetPath = OutputPath;
            return cleaner;
        }

        public async Task GenerateAsync()
        {
            // Flatten out all generators.
            // This includes all FileGenerators and all CompositeGenerators in the hierarchy.
            IEnumerable<IGenerator> Flatten(ICompositeGenerator composite) =>
                composite.GetGenerators().SelectMany(g => (g is ICompositeGenerator c) ? Flatten(c).Concat(new[] { g }) : new[] { g });

            var allGenerators = Flatten(this).ToList();


            var fileGenerators = allGenerators.OfType<IFileGenerator>().ToList();
            var compositeGenerators = allGenerators.OfType<ICompositeGenerator>().ToList();
            var outputtedFiles = fileGenerators.Select(g => g.OutputPath).ToList();
            var cleaners = compositeGenerators.SelectMany(g => g.GetCleaners()).ToList();


            Logger.LogDebug($"Generating {fileGenerators.Count} files from {compositeGenerators.Count} composites");
            await Task.WhenAll(fileGenerators
                .AsParallel()
                .Select(g => g.GenerateAsync()));


            Logger.LogDebug($"Running {cleaners.Count} cleaners");
            await Task.WhenAll(cleaners.Select(c => c.CleanupAsync(outputtedFiles)).ToList());
        }

        public abstract IEnumerable<IGenerator> GetGenerators();
        public virtual IEnumerable<ICleaner> GetCleaners() => Enumerable.Empty<ICleaner>();

        // public virtual bool ShouldGenerate(IFileSystem fileSystem) => true;

        public virtual bool Validate() => true;

        public override string ToString()
        {
            if (OutputPath != null)
            {
                return $"{this.GetType().Name} => {OutputPath}";
            }

            return this.GetType().Name;
        }
    }
}