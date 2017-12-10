using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Orchestrations.Contracts.Leave
{
    /// <summary>
    /// The LeaveRequestCollection Class.
    /// </summary>
    public class LeaveRequestCollection
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IEnumerable<LeaveRequestModel> Requests { get; set; }
    }
}
