using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeavePortal.Web.Models
{
    public class PendingLeaveViewModel
    {
        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        public int RequestId { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the requested leave in days.
        /// </summary>
        /// <value>
        /// The requested leave in days.
        /// </value>
        public decimal RequestedLeaveInDays { get; set; }

        /// <summary>
        /// Gets or sets the leave balance in days.
        /// </summary>
        /// <value>
        /// The leave balance in days.
        /// </value>
        public decimal LeaveBalanceInDays { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the date requested.
        /// </summary>
        /// <value>
        /// The date requested.
        /// </value>
        public DateTime DateRequested { get; set; }

        /// <summary>
        /// Gets or sets the type of the leave.
        /// </summary>
        /// <value>
        /// The type of the leave.
        /// </value>
        public string LeaveType { get; set; }
    }
}