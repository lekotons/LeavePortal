using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Orchestrations.Contracts.Leave
{
    public class LeaveHistoryModel : LeaveRequestModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [leave cancelled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [leave cancelled]; otherwise, <c>false</c>.
        /// </value>
        public bool LeaveCancelled { get; set; }
    }
}
