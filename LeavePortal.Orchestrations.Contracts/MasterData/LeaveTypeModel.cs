using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Orchestrations.Contracts.MasterData
{
    /// <summary>
    /// The LeaveType Class.
    /// </summary>
    public class LeaveTypeModel
    {
        /// <summary>
        /// Gets or sets the leave type identifier.
        /// </summary>
        /// <value>
        /// The leave type identifier.
        /// </value>
        public int LeaveTypeId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
