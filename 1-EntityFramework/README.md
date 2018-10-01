# Note on Entity Framework

## History of EF

## EF Core

EF Core is the recommended data access technology for new applications and for application 

With EF Core, __data access__ is performed using a `model`. A model is made up of __entity classes__ and a derived __context__ that represents a `session` with the database, allowing you to __query__ and __save__ data. We will be looking into models through out this course.

## Command-Line Reference

Entity Framework provides `command line tooling` to _automate_ common tasks such as __code generation__ and __database migrations__. We will be using some of these commands throughout this course. There are two primary command line experiences that EF Core provides.

1. [Package Manager Console](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell) Tools provide command line tools integrated into Visual Studio.

2. [.NET Core CLI](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet) Tools provide IDE-independent and cross-platform tools for applications being developed with .NET Core. This is the CLI tools we are going to use in this course. In the tutorial labs of this module, you will be installing the .Net Core CLI tools. For the complete reference of EF Core commands check the previous link.

-------------------------------------------------------

## 1. Create initial project demo

To get started lets see how to create an initial project with EF Core and have all the needed packages installed so you can get started worked with MySql and EF Core.

These are the dot net commands demonestrated in the video:

dotnet add package - is the command that adds a package reference to a project file.

```PowerShell
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet
dotnet add package Pomelo.EntityFrameworkCore.MySql -v 2.0.0-rtm-10059
dotnet add package Pomelo.EntityFrameworkCore.MySql.Design -v 1.1.2
```

Note: Check the tutorial labs in this module which provides you with a step by step activity to install the EF Core on your machine and get started with your initial project.

    https://www.youtube.com/watch?time_continue=1&v=F2XF9awAYn0

## 2. Importing Schema from existing DB

In many cases, you will build an application that __performs__ `data access operations` against an existing database. You can use `EF Core` to do the __reverse engineering__ to create an __Entity Framework__ `model` based on the existing database. This is called __Scaffolding__.

This demo will show you how to use the Scaffolding command to __import__ a `schema` from an existing database and have `EF Core` create the model for your application which includes .Net classes representing the entities in the database in addition to the context class needed to use for data access. The context class will inherit from `System.Data.Entity.DbContext`.

Also check Lab 2 in this module for hands on activities on using the Scaffolding command.

## Creating Entities with EF core

In the previous lesson, we looked at how to generate the EF model from an existing database. In this lesson we will be looking at the process from the opposite direction. When you have an application and you need to create a database for it using EF Core, you will be taking a `code-first approach` meaning that you will be creating the `model` first. From that model, `EF Core` will **generate** the database `tables` and **sets** your application up to interact with the data.

Remember that `EF Core` achieves data access by using a model and that the model is composed of two main parts, the context and the entity classes.

The entities (or entity sets) are basically the tables you have (or will have) in the database which represent a certain concept. In your EF Core model, these entities will translate into classes or objects in the .Net application data model and in that sense, they are called Entity Classes.

For example, Lets say in your class enrollment website application you are taking a code-first approach targeting a new database and you are planning to have three tables. One table for students data, one for enrollment data and a third table for courses data. In the code-first approach you will be creating the model in your application first that includes the entity classes and the context. In this lesson we will be focusing on creating the entity classes portion of the model. In this example, you will be creating three entity classes for the three mentioned tables to be able to access their data in your application model.