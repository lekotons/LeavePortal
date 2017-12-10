using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Commons
{
    public static class Errors
    {
        /// <summary>
        /// Gets or sets the internal error.
        /// </summary>
        /// <value>
        /// The internal error.
        /// </value>
        public static string InternalError
        {
            get
            {
                return "A internal error occurred.";
            }
        }

        /// <summary>
        /// Gets the pending leave.
        /// </summary>
        /// <value>
        /// The pending leave.
        /// </value>
        public static string PendingLeave
        {
            get
            {
                return "There is a pending leave which needs to be approved.";
            }
        }

        /// <summary>
        /// Gets the no available leave.
        /// </summary>
        /// <value>
        /// The no available leave.
        /// </value>
        public static string NoAvailableLeave
        {
            get
            {
                return "You have no available leave days.";
            }
        }
    }
}
