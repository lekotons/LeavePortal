using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeavePortal.Web.Models
{
    public class AwaitingApprovalViewModel
    {
        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        /// <value>
        /// The requests.
        /// </value>
        public IEnumerable<PendingLeaveViewModel> Requests { get; set; }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        /// <value>
        /// The history.
        /// </value>
        public IEnumerable<HistoryViewModel> History { get; set; }

    }
}