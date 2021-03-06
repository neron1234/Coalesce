@{ ViewBag.Title = "Coalesce"; Layout = "\_DocsLayout"; }

Overview
--------

What Coalesce Does
~~~~~~~~~~~~~~~~~~

Coalesce allows you to put your energy and creativity into the parts of
your application where they are needed most. With Coalesce you can worry
less about API plumbing, security, callback, data binding, view models,
controls, enumeration mapping, saving, and serialization. Instead, you
get to focus on applying your knowledge to key issues like data
modeling, business logic, page layout, form customizations, and the
custom functionality your customers are clamoring for.

How does it Work?
~~~~~~~~~~~~~~~~~

Underlying Coalesce is Microsoft's ASP.NET Core coupled with MVC Core
and Entity Framework Core. These are cutting edge Microsoft technologies
released for the first time in late 2015. Based on the lifecycle of the
last version of ASP.net, this means that the platforms should be stable
for 10+ years, a long time in the software world.

At the core of Coalesce is the .net Roslyn compiler. You build EF class
models of your data. These are just regular classes with some special
annotations for security, display, and other accoutrements. Then a
simple command is executed that builds out your project. This includes
APIs, Knockout view models, administrative views, and project setup
information. From here you can run your application to see web views of
all your models. Every time you change your model, you run the command
again. The great thing about this approach is that you have all the
code. We specifically tried to limit the amount of magic involved as to
not box developers into a corner. The generated code is designed to be
extended and has been carefully crafted to ensure this is possible.

Let's see a picture
~~~~~~~~~~~~~~~~~~~

Here is a block diagram that shows the main components of Coalesce

|image0|

.. |image0| image:: ~/img/coalescediagram.png

