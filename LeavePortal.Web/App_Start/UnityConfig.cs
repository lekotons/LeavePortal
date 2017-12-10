using LeavePortal.Adapters;
using LeavePortal.Adapters.Contracts;
using LeavePortal.Orchestrations;
using LeavePortal.Orchestrations.Contracts.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace LeavePortal.Web.App_Start
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
             container.RegisterType<ILeaveOrchestration, LeaveOrchestration>();
             container.RegisterType<ILeaveAdapter, LeaveAdapter>();
             container.RegisterType<IEmployeeAdapter, EmployeeAdapter>();
             container.RegisterType<IMasterDataAdapter, MasterDataAdapter>();

            //container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            //container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            //container.RegisterType<IAuthenticationManager>(new InjectionFactory(x => HttpContext.Current.GetOwinContext().Authentication));
            //container.RegisterType(typeof(IUserStore<ApplicationUser>), typeof(UserStore<ApplicationUser>));
            //container.RegisterType<AccountController>(new InjectionConstructor());
        }
    }
}