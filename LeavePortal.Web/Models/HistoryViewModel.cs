using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeavePortal.Web.Models
{
    public class HistoryViewModel : PendingLeaveViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HistoryViewModel"/> is cancelled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cancelled; otherwise, <c>false</c>.
        /// </value>
        public bool Cancelled { get; set; }
    }
}