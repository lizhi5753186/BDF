using System;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using Bdf.Configuration.Startup;
using Bdf.Dependency;
using Bdf.Dependency.Installers;
using Bdf.Events.Bus.Entities;
using Bdf.Events.Bus.Installers;
using Bdf.Interceptions;
using Bdf.NHibernate.Interceptors;
using Bdf.NHibernate.Uow;
using Bdf.Tests;
using Bdf.Uow;
using Castle.MicroKernel.Registration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Bdf.NHibernate.Tests
{
    public class NHibernateTestBase : TestBaseWithLocalIocManager
    {
        private readonly SQLiteConnection _connection;
        public FluentConfiguration FluentConfiguration { get; private set; }

        protected NHibernateTestBase()
        {
            _connection = new SQLiteConnection("data source=:memory:");
            _connection.Open();

            LocalIocManager.IocContainer.Register(
                Component.For<IDbConnection>().UsingFactoryMethod(() => _connection));

            LocalIocManager.IocContainer.Install(new BdfCoreInstaller());
            UnitOfWorkRegistrar.Initialize(LocalIocManager);

            LocalIocManager.IocContainer.Install(new EventBusInstaller(LocalIocManager));
            LocalIocManager.IocContainer.Register(
                Component.For<ICurrentUnitOfWorkProvider>().ImplementedBy<DefaultCurrentUnitOfWorkProvider>().LifestyleSingleton(),
                Component.For<IUnitOfWorkManager>().ImplementedBy<UnitOfWorkManager>().LifestyleSingleton(),
                Component.For<IUnitOfWork>().ImplementedBy<NhUnitOfWork>().LifestyleTransient());

            LocalIocManager.Register<IEntityChangedEventHelper, DefaultEntityChangedEventHelper>();
            LocalIocManager.Register<BdfNHibernateInterceptor>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<UnitOfWorkInterceptor>(DependencyLifeStyle.Transient);
            LocalIocManager.Register<ISessionProvider, UnitOfWorkSessionProvider>();
           
            FluentConfiguration = Fluently.Configure();
            FluentConfiguration
                .Database(SQLiteConfiguration.Standard.InMemory())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false, LocalIocManager.Resolve<IDbConnection>(), Console.Out));
        }

        public void UsingSession(Action<ISession> action)
        {
            using (var session = LocalIocManager.Resolve<ISessionFactory>().OpenSession(_connection))
            {
                using (var transaction = session.BeginTransaction())
                {
                    action(session);
                    session.Flush();
                    transaction.Commit();
                }
            }
        }

        public T UsingSession<T>(Func<ISession, T> func)
        {
            T result;

            using (var session = LocalIocManager.Resolve<ISessionFactory>().OpenSession(_connection))
            {
                using (var transaction = session.BeginTransaction())
                {
                    result = func(session);
                    session.Flush();
                    transaction.Commit();
                }
            }

            return result;
        }

        public override void Dispose()
        {
            _connection.Dispose();
            base.Dispose();
        }
    }
}