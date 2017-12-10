using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Orchestrations.Contracts.Leave
{
    /// <summary>
    /// LeaveHistoryCollection Class.
    /// </summary>
    public class LeaveHistoryCollection
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IEnumerable<LeaveHistoryModel> History { get; set; }
    }
}
