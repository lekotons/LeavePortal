using LeavePortal.Adapters.Contracts;
using LeavePortal.Orchestrations.Contracts.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Orchestrations
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="LeavePortal.Orchestrations.Contracts.MasterData.IMasterDataOrchestration" />
    public class MasterDataOrchestration : IMasterDataOrchestration
    {
        /// <summary>
        /// The master data adapter
        /// </summary>
        private readonly IMasterDataAdapter masterDataAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterDataOrchestration"/> class.
        /// </summary>
        /// <param name="masterDataAdapter">The master data adapter.</param>
        public MasterDataOrchestration(IMasterDataAdapter masterDataAdapter)
        {
            this.masterDataAdapter = masterDataAdapter;
        }

        /// <summary>
        /// Gets the leave types.
        /// </summary>
        /// <returns>
        /// A collection of leave types.
        /// </returns>
        public LeaveTypeCollection GetLeaveTypes()
        {
            var dataOutcome = this.masterDataAdapter.GetLeaveTypes();
            return dataOutcome;
        }
    }
}
