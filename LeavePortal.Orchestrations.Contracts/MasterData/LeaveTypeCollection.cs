using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Orchestrations.Contracts.MasterData
{
    /// <summary>
    /// The LeaveTypeCollection Class.
    /// </summary>
    public class LeaveTypeCollection
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IEnumerable<LeaveTypeModel> Data { get; set; }
    }
}
