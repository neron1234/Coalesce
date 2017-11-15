﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IntelliTect.Coalesce.TypeDefinition.Wrappers
{
    internal class SymbolPropertyViewModel : PropertyViewModel
    {
        protected IPropertySymbol Symbol;

        public SymbolPropertyViewModel(ClassViewModel parent, IPropertySymbol symbol)
        {
            Parent = parent;
            Symbol = symbol;
            Type = new SymbolTypeViewModel(Symbol.Type);
        }

        public override string Name => Symbol.Name;

        public override string Comment =>
            Regex.Replace(SymbolHelper.ExtractXmlComments(Symbol), "\n(\\s+)", "\n        // ");

        public override object GetAttributeValue<TAttribute>(string valueName) =>
            Symbol.GetAttributeValue<TAttribute>(valueName);
        
        public override bool HasAttribute<TAttribute>() => Symbol.HasAttribute<TAttribute>();
        
        public override bool HasGetter => !Symbol.IsWriteOnly;

        public override bool HasSetter => !Symbol.IsReadOnly;

        public override bool IsStatic => Symbol.IsStatic;

        public override bool IsVirtual => Symbol.IsVirtual;

        public override bool IsInternalUse => base.IsInternalUse || Symbol.DeclaredAccessibility != Accessibility.Public;

    }
}