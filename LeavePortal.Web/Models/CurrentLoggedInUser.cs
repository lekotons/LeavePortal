using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeavePortal.Web.Models
{
    /// <summary>
    /// The CurrentLoggedInUser Class.
    /// </summary>
    public class CurrentLoggedInUser
    {
        /// <summary>
        /// Gets the name and surname.
        /// </summary>
        /// <value>
        /// The name and surname.
        /// </value>
        public static string Name
        {
            get
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var name = HttpContext.Current.User.Identity.Name;
                return manager.FindByName(name).Name;
            }
        }

        /// <summary>
        /// Gets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public static int EmployeeId
        {
            get
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var name = HttpContext.Current.User.Identity.Name;
                var id = Convert.ToInt32(manager.FindByName(name).EmployeeId);

                return id;
            }
        }
    }
}
