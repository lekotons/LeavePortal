
namespace LeavePortal.Commons
{
    using Microsoft.Practices.Unity.Configuration;
    using Unity;

    /// <summary>
    /// The Factory Class.
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
            var container = new UnityContainer();
            container.LoadConfiguration();

            var resolvedObject = container.Resolve<T>();

            return resolvedObject;
        }
    }
}
