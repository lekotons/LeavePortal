using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Orchestrations.Contracts.MasterData
{
    /// <summary>
    /// The IMasterDataOrchestration Class.
    /// </summary>
    public interface IMasterDataOrchestration
    {
        /// <summary>
        /// Gets the leave types.
        /// </summary>
        /// <returns>A collection of leave types.</returns>
        LeaveTypeCollection GetLeaveTypes();
    }
}
