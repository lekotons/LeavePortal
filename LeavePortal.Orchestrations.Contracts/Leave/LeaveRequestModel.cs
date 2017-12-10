using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Orchestrations.Contracts.Leave
{
    /// <summary>
    /// The LeaveRequest Class.
    /// </summary>
    public class LeaveRequestModel
    {
        /// <summary>
        /// Gets or sets the leave request identifier.
        /// </summary>
        /// <value>
        /// The leave request identifier.
        /// </value>
        public int LeaveRequestId { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the requested days.
        /// </summary>
        /// <value>
        /// The requested days.
        /// </value>
        public decimal RequestedDays { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the leave.
        /// </summary>
        /// <value>
        /// The type of the leave.
        /// </value>
        public string LeaveType { get; set; }

        /// <summary>
        /// Gets or sets the date requested.
        /// </summary>
        /// <value>
        /// The date requested.
        /// </value>
        public DateTime DateRequested { get; set; }
    }
}
