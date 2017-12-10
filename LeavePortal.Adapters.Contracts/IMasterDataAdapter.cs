using LeavePortal.Orchestrations.Contracts.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Adapters.Contracts
{
    /// <summary>
    /// The IMasterDataAdapter interface.
    /// </summary>
    public interface IMasterDataAdapter
    {
        /// <summary>
        /// Gets the holiday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        HolidayModel GetHoliday(DateTime date);

        /// <summary>
        /// Gets the leave types.
        /// </summary>
        /// <returns>A collection of leave types.</returns>
        LeaveTypeCollection GetLeaveTypes();
    }
}
