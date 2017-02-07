﻿

// ------------------------------------------------------------------------------------------------
// This code was generated by EntityFramework Reverse POCO Generator (http://www.reversepoco.com/).
// Created by Simon Hughes (https://about.me/simon.hughes).
//
// Do not make changes directly to this file - edit the template instead.
//
// The following connection settings were used to generate this file:
//     Configuration file:     "ClayOnWheels\Web.config"
//     Connection String Name: "DefaultConnection"
//     Connection String:      "Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\projects\ClayOnWheels\ClayOnWheels\ClayOnWheels\App_Data\\aspnet-ClayOnWheels-20161107075613.mdf;Initial Catalog=aspnet-ClayOnWheels-20161107075613;Integrated Security=True"
// ------------------------------------------------------------------------------------------------
// Database Edition       : Express Edition (64-bit)
// Database Engine Edition: Express

// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.51

using System;
using System.ComponentModel.DataAnnotations;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace ClayOnWheels.Models.EF
{
    using System.Collections.Generic;
    using System.Linq;

    #region Unit of work

    public interface IMyDbContext : System.IDisposable
    {
        System.Data.Entity.DbSet<AppointmentDiary> AppointmentDiaries { get; set; } // AppointmentDiary
        System.Data.Entity.DbSet<AspNetRole> AspNetRoles { get; set; } // AspNetRoles
        System.Data.Entity.DbSet<AspNetUser> AspNetUsers { get; set; } // AspNetUsers
        System.Data.Entity.DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } // AspNetUserClaims
        System.Data.Entity.DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } // AspNetUserLogins
        System.Data.Entity.DbSet<Subscription> Subscriptions { get; set; } // Subscriptions
        System.Data.Entity.DbSet<UserSubscription> UserSubscriptions { get; set; } // UserSubscriptions

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
        System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get; }
        System.Data.Entity.Infrastructure.DbContextConfiguration Configuration { get; }
        System.Data.Entity.Database Database { get; }
        System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity);
        System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors();
        System.Data.Entity.DbSet Set(System.Type entityType);
        System.Data.Entity.DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
    }

    #endregion

    #region Database context

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class MyDbContext : System.Data.Entity.DbContext, IMyDbContext
    {
        public System.Data.Entity.DbSet<AppointmentDiary> AppointmentDiaries { get; set; } // AppointmentDiary
        public System.Data.Entity.DbSet<AspNetRole> AspNetRoles { get; set; } // AspNetRoles
        public System.Data.Entity.DbSet<AspNetUser> AspNetUsers { get; set; } // AspNetUsers
        public System.Data.Entity.DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } // AspNetUserClaims
        public System.Data.Entity.DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } // AspNetUserLogins
        public System.Data.Entity.DbSet<Subscription> Subscriptions { get; set; } // Subscriptions
        public System.Data.Entity.DbSet<UserSubscription> UserSubscriptions { get; set; } // UserSubscriptions

        static MyDbContext()
        {
            System.Data.Entity.Database.SetInitializer<MyDbContext>(null);
        }

        public MyDbContext()
            : base("Name=DefaultConnection")
        {
        }

        public MyDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public MyDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(connectionString, model)
        {
        }

        public MyDbContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public MyDbContext(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public bool IsSqlParameterNull(System.Data.SqlClient.SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as System.Data.SqlTypes.INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == System.DBNull.Value);
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AppointmentDiaryConfiguration());
            modelBuilder.Configurations.Add(new AspNetRoleConfiguration());
            modelBuilder.Configurations.Add(new AspNetUserConfiguration());
            modelBuilder.Configurations.Add(new AspNetUserClaimConfiguration());
            modelBuilder.Configurations.Add(new AspNetUserLoginConfiguration());
            modelBuilder.Configurations.Add(new SubscriptionConfiguration());
            modelBuilder.Configurations.Add(new UserSubscriptionConfiguration());
        }

        public static System.Data.Entity.DbModelBuilder CreateModel(System.Data.Entity.DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AppointmentDiaryConfiguration(schema));
            modelBuilder.Configurations.Add(new AspNetRoleConfiguration(schema));
            modelBuilder.Configurations.Add(new AspNetUserConfiguration(schema));
            modelBuilder.Configurations.Add(new AspNetUserClaimConfiguration(schema));
            modelBuilder.Configurations.Add(new AspNetUserLoginConfiguration(schema));
            modelBuilder.Configurations.Add(new SubscriptionConfiguration(schema));
            modelBuilder.Configurations.Add(new UserSubscriptionConfiguration(schema));
            return modelBuilder;
        }
    }
    #endregion

    #region Fake Database context

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class FakeMyDbContext : IMyDbContext
    {
        public System.Data.Entity.DbSet<AppointmentDiary> AppointmentDiaries { get; set; }
        public System.Data.Entity.DbSet<AspNetRole> AspNetRoles { get; set; }
        public System.Data.Entity.DbSet<AspNetUser> AspNetUsers { get; set; }
        public System.Data.Entity.DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public System.Data.Entity.DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public System.Data.Entity.DbSet<Subscription> Subscriptions { get; set; }
        public System.Data.Entity.DbSet<UserSubscription> UserSubscriptions { get; set; }

        public FakeMyDbContext()
        {
            AppointmentDiaries = new FakeDbSet<AppointmentDiary>("Id");
            AspNetRoles = new FakeDbSet<AspNetRole>("Id");
            AspNetUsers = new FakeDbSet<AspNetUser>("Id");
            AspNetUserClaims = new FakeDbSet<AspNetUserClaim>("Id");
            AspNetUserLogins = new FakeDbSet<AspNetUserLogin>("LoginProvider", "ProviderKey", "UserId");
            Subscriptions = new FakeDbSet<Subscription>("Id");
            UserSubscriptions = new FakeDbSet<UserSubscription>("Id");
        }

        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            ++SaveChangesCount;
            return System.Threading.Tasks.Task<int>.Factory.StartNew(() => 1);
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            ++SaveChangesCount;
            return System.Threading.Tasks.Task<int>.Factory.StartNew(() => 1, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get; }
        public System.Data.Entity.Infrastructure.DbContextConfiguration Configuration { get; }
        public System.Data.Entity.Database Database { get; }
        public System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new System.NotImplementedException();
        }
        public System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity)
        {
            throw new System.NotImplementedException();
        }
        public System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors()
        {
            throw new System.NotImplementedException();
        }
        public System.Data.Entity.DbSet Set(System.Type entityType)
        {
            throw new System.NotImplementedException();
        }
        public System.Data.Entity.DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new System.NotImplementedException();
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

    }

    // ************************************************************************
    // Fake DbSet
    // Implementing Find:
    //      The Find method is difficult to implement in a generic fashion. If
    //      you need to test code that makes use of the Find method it is
    //      easiest to create a test DbSet for each of the entity types that
    //      need to support find. You can then write logic to find that
    //      particular type of entity, as shown below:
    //      public class FakeBlogDbSet : FakeDbSet<Blog>
    //      {
    //          public override Blog Find(params object[] keyValues)
    //          {
    //              var id = (int) keyValues.Single();
    //              return this.SingleOrDefault(b => b.BlogId == id);
    //          }
    //      }
    //      Read more about it here: https://msdn.microsoft.com/en-us/data/dn314431.aspx
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class FakeDbSet<TEntity> : System.Data.Entity.DbSet<TEntity>, IQueryable, System.Collections.Generic.IEnumerable<TEntity>, System.Data.Entity.Infrastructure.IDbAsyncEnumerable<TEntity> where TEntity : class
    {
        private readonly System.Reflection.PropertyInfo[] _primaryKeys;
        private readonly System.Collections.ObjectModel.ObservableCollection<TEntity> _data;
        private readonly IQueryable _query;

        public FakeDbSet()
        {
            _data = new System.Collections.ObjectModel.ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public FakeDbSet(params string[] primaryKeys)
        {
            _primaryKeys = typeof(TEntity).GetProperties().Where(x => primaryKeys.Contains(x.Name)).ToArray();
            _data = new System.Collections.ObjectModel.ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public override TEntity Find(params object[] keyValues)
        {
            if (_primaryKeys == null)
                throw new System.ArgumentException("No primary keys defined");
            if (keyValues.Length != _primaryKeys.Length)
                throw new System.ArgumentException("Incorrect number of keys passed to Find method");

            var keyQuery = this.AsQueryable();
            keyQuery = keyValues
                .Select((t, i) => i)
                .Aggregate(keyQuery,
                    (current, x) =>
                        current.Where(entity => _primaryKeys[x].GetValue(entity, null).Equals(keyValues[x])));

            return keyQuery.SingleOrDefault();
        }

        public override System.Threading.Tasks.Task<TEntity> FindAsync(System.Threading.CancellationToken cancellationToken, params object[] keyValues)
        {
            return System.Threading.Tasks.Task<TEntity>.Factory.StartNew(() => Find(keyValues), cancellationToken);
        }

        public override System.Threading.Tasks.Task<TEntity> FindAsync(params object[] keyValues)
        {
            return System.Threading.Tasks.Task<TEntity>.Factory.StartNew(() => Find(keyValues));
        }

        public override System.Collections.Generic.IEnumerable<TEntity> AddRange(System.Collections.Generic.IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new System.ArgumentNullException("entities");
            var items = entities.ToList();
            foreach (var entity in items)
            {
                _data.Add(entity);
            }
            return items;
        }

        public override TEntity Add(TEntity item)
        {
            if (item == null) throw new System.ArgumentNullException("item");
            _data.Add(item);
            return item;
        }

        public override System.Collections.Generic.IEnumerable<TEntity> RemoveRange(System.Collections.Generic.IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new System.ArgumentNullException("entities");
            var items = entities.ToList();
            foreach (var entity in items)
            {
                _data.Remove(entity);
            }
            return items;
        }

        public override TEntity Remove(TEntity item)
        {
            if (item == null) throw new System.ArgumentNullException("item");
            _data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            if (item == null) throw new System.ArgumentNullException("item");
            _data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return System.Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return System.Activator.CreateInstance<TDerivedEntity>();
        }

        public override System.Collections.ObjectModel.ObservableCollection<TEntity> Local
        {
            get { return _data; }
        }

        System.Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new FakeDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        System.Collections.Generic.IEnumerator<TEntity> System.Collections.Generic.IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        System.Data.Entity.Infrastructure.IDbAsyncEnumerator<TEntity> System.Data.Entity.Infrastructure.IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return new FakeDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class FakeDbAsyncQueryProvider<TEntity> : System.Data.Entity.Infrastructure.IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public FakeDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
        {
            return new FakeDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
        {
            return new FakeDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(System.Linq.Expressions.Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public System.Threading.Tasks.Task<object> ExecuteAsync(System.Linq.Expressions.Expression expression, System.Threading.CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute(expression));
        }

        public System.Threading.Tasks.Task<TResult> ExecuteAsync<TResult>(System.Linq.Expressions.Expression expression, System.Threading.CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute<TResult>(expression));
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class FakeDbAsyncEnumerable<T> : EnumerableQuery<T>, System.Data.Entity.Infrastructure.IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public FakeDbAsyncEnumerable(System.Collections.Generic.IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public FakeDbAsyncEnumerable(System.Linq.Expressions.Expression expression)
            : base(expression)
        { }

        public System.Data.Entity.Infrastructure.IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new FakeDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        System.Data.Entity.Infrastructure.IDbAsyncEnumerator System.Data.Entity.Infrastructure.IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new FakeDbAsyncQueryProvider<T>(this); }
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class FakeDbAsyncEnumerator<T> : System.Data.Entity.Infrastructure.IDbAsyncEnumerator<T>
    {
        private readonly System.Collections.Generic.IEnumerator<T> _inner;

        public FakeDbAsyncEnumerator(System.Collections.Generic.IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public System.Threading.Tasks.Task<bool> MoveNextAsync(System.Threading.CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object System.Data.Entity.Infrastructure.IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }

    #endregion

    #region POCO classes

    // AppointmentDiary
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AppointmentDiary
    {
        public int Id { get; set; } // ID (Primary key)
        public string Title { get; set; } // Title (length: 100)
        public int SomeImportantKey { get; set; } // SomeImportantKey
        public System.DateTime DateTimeScheduled { get; set; } // DateTimeScheduled
        public int AppointmentLength { get; set; } // AppointmentLength
        public int StatusEnum { get; set; } // StatusENUM
        public virtual System.Collections.Generic.ICollection<UserSubscription> UserSubscriptions { get; set; } // UserSubscriptions.FK_dbo.UserSubscriptions.AspNetUsers_UserId

        public AppointmentDiary()
        {
            StatusEnum = 0;
            UserSubscriptions = new System.Collections.Generic.List<UserSubscription>();
        }
    }

    // AspNetRoles
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AspNetRole
    {
        public string Id { get; set; } // Id (Primary key) (length: 128)
        public string Name { get; set; } // Name (length: 256)

        // Reverse navigation
        public virtual System.Collections.Generic.ICollection<AspNetUser> AspNetUsers { get; set; } // Many to many mapping

        public AspNetRole()
        {
            AspNetUsers = new System.Collections.Generic.List<AspNetUser>();
        }
    }

    // AspNetUsers
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AspNetUser
    {
        public string Id { get; set; } // Id (Primary key) (length: 128)
        public string Email { get; set; } // Email (length: 256)
        public bool EmailConfirmed { get; set; } // EmailConfirmed
        public string PasswordHash { get; set; } // PasswordHash
        public string SecurityStamp { get; set; } // SecurityStamp
        public string PhoneNumber { get; set; } // PhoneNumber
        public bool PhoneNumberConfirmed { get; set; } // PhoneNumberConfirmed
        public bool TwoFactorEnabled { get; set; } // TwoFactorEnabled
        public System.DateTime? LockoutEndDateUtc { get; set; } // LockoutEndDateUtc
        public bool LockoutEnabled { get; set; } // LockoutEnabled
        public int AccessFailedCount { get; set; } // AccessFailedCount
        public string UserName { get; set; } // UserName (length: 256)
        public string FirstName { get; set; } // UserName (length: 256)
        public string LastName { get; set; } // UserName (length: 256)
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool Active { get; set; }

        // Reverse navigation
        public virtual System.Collections.Generic.ICollection<AspNetRole> AspNetRoles { get; set; } // Many to many mapping
        public virtual System.Collections.Generic.ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } // AspNetUserClaims.FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId
        public virtual System.Collections.Generic.ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } // Many to many mapping
        public virtual System.Collections.Generic.ICollection<Subscription> Subscriptions { get; set; } // Subscriptions.FK_dbo.Subscriptions_dbo.AspNetUsers_UserId
        public virtual System.Collections.Generic.ICollection<UserSubscription> UserSubscriptions { get; set; } // UserSubscriptions.FK_dbo.UserSubscriptions.AspNetUsers_UserId

        public AspNetUser()
        {
            AspNetUserClaims = new System.Collections.Generic.List<AspNetUserClaim>();
            AspNetUserLogins = new System.Collections.Generic.List<AspNetUserLogin>();
            Subscriptions = new System.Collections.Generic.List<Subscription>();
            UserSubscriptions = new System.Collections.Generic.List<UserSubscription>();
            AspNetRoles = new System.Collections.Generic.List<AspNetRole>();
        }
    }

    // AspNetUserClaims
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AspNetUserClaim
    {
        public int Id { get; set; } // Id (Primary key)
        public string UserId { get; set; } // UserId (length: 128)
        public string ClaimType { get; set; } // ClaimType
        public string ClaimValue { get; set; } // ClaimValue

        // Foreign keys
        public virtual AspNetUser AspNetUser { get; set; } // FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId
    }

    // AspNetUserLogins
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AspNetUserLogin
    {
        public string LoginProvider { get; set; } // LoginProvider (Primary key) (length: 128)
        public string ProviderKey { get; set; } // ProviderKey (Primary key) (length: 128)
        public string UserId { get; set; } // UserId (Primary key) (length: 128)

        // Foreign keys
        public virtual AspNetUser AspNetUser { get; set; } // FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId
    }

    // Subscriptions
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class Subscription
    {
        public int Id { get; set; } // Id (Primary key)
        public string UserId { get; set; } // UserId (length: 128)
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DatePurchased { get; set; } // DatePurchased
        public int Number { get; set; } // Number

        // Reverse navigation
       // public virtual Subscription Subscription1 { get; set; } // Subscriptions.FK_Subscriptions_Subscriptions

        // Foreign keys
        public virtual AspNetUser AspNetUser { get; set; } // FK_dbo.Subscriptions_dbo.AspNetUsers_UserId
       // public virtual Subscription Subscription_Id { get; set; } // FK_Subscriptions_Subscriptions
    }

    // UserSubscriptions
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class UserSubscription
    {
        public int Id { get; set; } // Id (Primary key)
        public string UserId { get; set; } // UserId (length: 128)
        public int AppointmentDairyId { get; set; } // AppointmentDairyId
        public DateTime Created { get; set; } // DateCreated
        public int Pending { get; set; } // Pending
                                         // Foreign keys

        public virtual AspNetUser AspNetUser { get; set; } // FK_dbo.UserSubscriptions.AspNetUsers_UserId
        public virtual AppointmentDiary AppointmentDiary { get; set; }
    }

    #endregion

    #region POCO Configuration

    // AppointmentDiary
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AppointmentDiaryConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AppointmentDiary>
    {
        public AppointmentDiaryConfiguration()
            : this("dbo")
        {
        }

        public AppointmentDiaryConfiguration(string schema)
        {
            ToTable("AppointmentDiary", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"ID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Title).HasColumnName(@"Title").HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(x => x.SomeImportantKey).HasColumnName(@"SomeImportantKey").HasColumnType("int").IsRequired();
            Property(x => x.DateTimeScheduled).HasColumnName(@"DateTimeScheduled").HasColumnType("datetime").IsRequired();
            Property(x => x.AppointmentLength).HasColumnName(@"AppointmentLength").HasColumnType("int").IsRequired();
            Property(x => x.StatusEnum).HasColumnName(@"StatusENUM").HasColumnType("int").IsRequired();
        }
    }

    // AspNetRoles
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AspNetRoleConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AspNetRole>
    {
        public AspNetRoleConfiguration()
            : this("dbo")
        {
        }

        public AspNetRoleConfiguration(string schema)
        {
            ToTable("AspNetRoles", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("nvarchar").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            HasMany(t => t.AspNetUsers).WithMany(t => t.AspNetRoles).Map(m =>
            {
                m.ToTable("AspNetUserRoles", "dbo");
                m.MapLeftKey("RoleId");
                m.MapRightKey("UserId");
            });
        }
    }

    // AspNetUsers
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AspNetUserConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AspNetUser>
    {
        public AspNetUserConfiguration()
            : this("dbo")
        {
        }

        public AspNetUserConfiguration(string schema)
        {
            ToTable("AspNetUsers", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("nvarchar").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Email).HasColumnName(@"Email").HasColumnType("nvarchar").IsOptional().HasMaxLength(256);
            Property(x => x.EmailConfirmed).HasColumnName(@"EmailConfirmed").HasColumnType("bit").IsRequired();
            Property(x => x.PasswordHash).HasColumnName(@"PasswordHash").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.SecurityStamp).HasColumnName(@"SecurityStamp").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.PhoneNumber).HasColumnName(@"PhoneNumber").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.PhoneNumberConfirmed).HasColumnName(@"PhoneNumberConfirmed").HasColumnType("bit").IsRequired();
            Property(x => x.TwoFactorEnabled).HasColumnName(@"TwoFactorEnabled").HasColumnType("bit").IsRequired();
            Property(x => x.LockoutEndDateUtc).HasColumnName(@"LockoutEndDateUtc").HasColumnType("datetime").IsOptional();
            Property(x => x.LockoutEnabled).HasColumnName(@"LockoutEnabled").HasColumnType("bit").IsRequired();
            Property(x => x.AccessFailedCount).HasColumnName(@"AccessFailedCount").HasColumnType("int").IsRequired();
            Property(x => x.UserName).HasColumnName(@"UserName").HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
        }
    }

    // AspNetUserClaims
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AspNetUserClaimConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AspNetUserClaim>
    {
        public AspNetUserClaimConfiguration()
            : this("dbo")
        {
        }

        public AspNetUserClaimConfiguration(string schema)
        {
            ToTable("AspNetUserClaims", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("nvarchar").IsRequired().HasMaxLength(128);
            Property(x => x.ClaimType).HasColumnName(@"ClaimType").HasColumnType("nvarchar(max)").IsOptional();
            Property(x => x.ClaimValue).HasColumnName(@"ClaimValue").HasColumnType("nvarchar(max)").IsOptional();

            // Foreign keys
            HasRequired(a => a.AspNetUser).WithMany(b => b.AspNetUserClaims).HasForeignKey(c => c.UserId); // FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId
        }
    }

    // AspNetUserLogins
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class AspNetUserLoginConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AspNetUserLogin>
    {
        public AspNetUserLoginConfiguration()
            : this("dbo")
        {
        }

        public AspNetUserLoginConfiguration(string schema)
        {
            ToTable("AspNetUserLogins", schema);
            HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });

            Property(x => x.LoginProvider).HasColumnName(@"LoginProvider").HasColumnType("nvarchar").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.ProviderKey).HasColumnName(@"ProviderKey").HasColumnType("nvarchar").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("nvarchar").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            // Foreign keys
            HasRequired(a => a.AspNetUser).WithMany(b => b.AspNetUserLogins).HasForeignKey(c => c.UserId); // FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId
        }
    }

    // Subscriptions
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class SubscriptionConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Subscription>
    {
        public SubscriptionConfiguration()
            : this("dbo")
        {
        }

        public SubscriptionConfiguration(string schema)
        {
            ToTable("Subscriptions", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("nvarchar").IsRequired().HasMaxLength(128);
            Property(x => x.DatePurchased).HasColumnName(@"DatePurchased").HasColumnType("datetime").IsRequired();
            Property(x => x.Number).HasColumnName(@"Number").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.AspNetUser).WithMany(b => b.Subscriptions).HasForeignKey(c => c.UserId); // FK_dbo.Subscriptions_dbo.AspNetUsers_UserId
           // HasRequired(a => a.Subscription_Id).WithOptional(b => b.Subscription1).WillCascadeOnDelete(false); // FK_Subscriptions_Subscriptions
        }
    }

    // UserSubscriptions
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.25.0.0")]
    public class UserSubscriptionConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserSubscription>
    {
        public UserSubscriptionConfiguration()
            : this("dbo")
        {
        }

        public UserSubscriptionConfiguration(string schema)
        {
            ToTable("UserSubscriptions", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("nvarchar").IsRequired().HasMaxLength(128);
            Property(x => x.AppointmentDairyId).HasColumnName(@"AppointmentDairyId").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.AspNetUser).WithMany(b => b.UserSubscriptions).HasForeignKey(c => c.UserId); // FK_dbo.UserSubscriptions.AspNetUsers_UserId
            HasRequired(a => a.AppointmentDiary).WithMany(b => b.UserSubscriptions).HasForeignKey(c => c.AppointmentDairyId); // FK_dbo.UserSubscriptions.AspNetUsers_UserId
        }
    }

    #endregion

}
// </auto-generated>

