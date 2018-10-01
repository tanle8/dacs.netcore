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

Remember that `EF Core` achieves __data access__ by using a `model` and that the `model` is composed of *two* main parts, the `context` and the `entity classes`.

The `entities` (or `entity sets`) are basically the `tables` you have (or will have) in the database which represent a certain concept. In your EF Core `model`, these entities will __translate__ into `classes` or `objects` in the .Net application `data model` and in that sense, they are called `Entity Classes`.

For example, Lets say in your class enrollment website application you are taking a `code-first approach` targeting a new database and you are planning to have *three* tables:

- One table for `students data`,
- One for `enrollment data` and
- A third table for `courses data`.

In the code-first approach you will be creating the model in your application first that includes the `entity classes` and the context. In this lesson we will be focusing on creating the `entity classes` portion of the `model`. In this example, you will be creating three entity classes for the three mentioned tables to be able to access their data in your application model.

![Data Model Diagram](https://prod-edxapp.edx-cdn.org/assets/courseware/v1/af2cb5873db1e68a770511c145762ba2/asset-v1:Microsoft+DEV258x+2T2018+type@asset+block/data-model-diagram.png)

```c#
// The Student Entity Class
///////////////////////////
public class Student
{
    public int ID { get; set; }  //this is the primary key of the Student table
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }

   //Enrollments is a navigation property to gold enrollment data found in an other table
    public ICollection<Enrollment> Enrollments { get; set; }
}
```

- The `ID` property will become the _primary key_ `column` of the database table that corresponds to this class. By default, the Entity Framework interprets a property that's named `ID` or classnameID as the primary key.

The `Enrollments` property is a **navigation property**. Navigation properties hold _other_ entities that are related to this entity. In this case, the `Enrollments` property of a `Student` entity will hold all of the Enrollment entities that are related to that `Student` entity. In other words, if a given Student row in the database has two related Enrollment rows, that Student entity's Enrollments navigation property will contain those two Enrollment entities.

If a navigation property can hold *multiple* `entities` (as in `many-to-many` or `one-to-many` relationships), its type must be a _list_ in which `entries` can be _added_, _deleted_, and _updated_, such as `ICollection<T>`. You can specify `ICollection<T>` or a type such as `List<T>` or `HashSet<T>`. If you specify ICollection<T>, EF creates a `HashSet<T>` collection by default.

```c#
// The Enrollment Entity Class
///////////////////////////
   public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }  //foreign key
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
```

- The `EnrollmentID` property will be the primary key;

    - this entity uses the `classnameID` pattern instead of `ID` by itself as you saw in the `Student` entity.
    - Ordinarily you would choose one pattern and use it throughout your `data model`. Here, the variation illustrates that you can use either pattern.

__Note__: Using `ID` without classname makes it easier to implement inheritance in the data model.

- The `Grade` property is an `enum`. The question mark after the Grade type declaration indicates that the _Grade property is nullable_. A grade that's null is different from a zero grade – _null means a grade isn't known or hasn't been assigned yet_, it is an __empty field__ in the `table row`.

The _StudentID_ property is a `foreign key`, and the corresponding navigation property is _Student_. An _Enrollment_ entity is associated with one Student entity, so the property can only hold a single Student entity (unlike the `Student.Enrollments` navigation property you saw earlier, which can hold multiple Enrollment entities).

The _CourseID_ property is a `foreign key`, and the corresponding navigation property is _Course_. An _Enrollment_ entity is associated with one _Course_ entity.

Note: Entity Framework interprets a property as a `foreign key` property if it's named `<navigation property name><primary key property name>` (for example, StudentID for the Student navigation property since the Student entity's primary key is ID). Foreign key properties can also be named simply `<primary key property name>` (for example, CourseID since the Course entity's primary key is CourseID).

```c#
// The Course Entity Class
///////////////////////////////
using System.ComponentModel.DataAnnotations.Schema;

public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
```

The `Enrollments` property is a navigation property. A `Course` entity can be related to any number of `Enrollment` entities.

The `DatabaseGenerated` attribute lets you enter the _primary key_ for the course rather than having the database generate it (not _auto generated_).

In relational databases entities are related with different types of relationships. We touch based on such relationships in this lesson. We will learn about these types of relationships in more details in module 3 of this course.

## Create the Database Context

The main class that coordinates Entity Framework functionality for a given data model is the database context class. You create this class by deriving from the `Microsoft.EntityFrameworkCore.DbContext` class. This derived context represents a session with the database, allowing you to _query_ and _save_ data. In your code you specify which `entity classes` are included in the data model by exposing a typed `DbSet<TEntity>` for each class in your model. You can also customize certain Entity Framework behavior. For the same class enrollment example we have been looking at in the previous lesson, we will create a DBContext class named SchoolContext.

For our school example from last lesson here is how the SchoolContext class should look like:

```c#
using Microsoft.EntityFrameworkCore;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }
}
```

This code creates a `DbSet` property for each entity set. In Entity Framework terminology, an entity set typically corresponds to a database table, and an entity corresponds to a row in the table.

Note: You could have omitted the DbSet<Enrollment> and DbSet<Course> statements and it would work the same. The Entity Framework would include them implicitly because the Student entity references the Enrollment entity and the Enrollment entity references the Course entity. This has been explained in the previous lesson when we created the entity classes.

When the database is created, EF creates tables that have names the same as the DbSet property names. Property names for collections are typically plural (Students rather than Student), but developers disagree about whether table names should be pluralized or not. EF Core gives you the option to override the default behavior by specifying different table names than the corresponding DbSet names in the DbContext. To do that, you will override the OnModelCreating method of the DbContext parent class. Add the following highlighted code after the last DbSet property.

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Course>().ToTable("Course");
    modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
    modelBuilder.Entity<Student>().ToTable("Student");
}
Your final code for the SchoolContext class will look like this:

using Microsoft.EntityFrameworkCore;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable("Course");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        modelBuilder.Entity<Student>().ToTable("Student");
    }
}
As you have seen, Entity Framework uses a set of conventions to build a model based on the shape of your entity classes. You can specify additional configuration to supplement and/or override what was discovered by convention. There are two main methods for configuring a model.

Methods of model configuration
Fluent API
You can override the OnModelCreating method in your derived context and use the ModelBuilder API to configure your model. This is the most powerful method of configuration and allows configuration to be specified without modifying your entity classes. Fluent API configuration has the highest precedence and will override conventions and data annotations.
class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(b => b.Url)
                .IsRequired();
        }
    }
Data Annotations
You can also apply attributes (known as Data Annotations) to your classes and properties. Data annotations will override conventions, but will be overwritten by Fluent API configuration.
public class Blog
    {
        public int BlogId { get; set; }
        [Required]
        public string Url { get; set; }
    }
That is all the code you need to start storing and retrieving data. There is quite a bit going on behind the scenes and we’ll take a look at that as we proceed in the course but first let’s see it in action by doing some basic data operations.