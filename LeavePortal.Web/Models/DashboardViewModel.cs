using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeavePortal.Web.Models
{
    public class DashboardViewModel
    {
        /// <summary>
        /// Gets or sets the balance model.
        /// </summary>
        /// <value>
        /// The balance model.
        /// </value>
        public LeaveBalanceViewModel BalanceModel { get; set; }

        /// <summary>
        /// Gets or sets the pending model.
        /// </summary>
        /// <value>
        /// The pending model.
        /// </value>
        public PendingLeaveViewModel PendingModel { get; set; }

        /// <summary>
        /// Gets or sets the history model.
        /// </summary>
        /// <value>
        /// The history model.
        /// </value>
        public IEnumerable<HistoryViewModel> HistoryModel { get; set; }
    }
}