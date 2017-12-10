using LeavePortal.Adapters.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeavePortal.Orchestrations.Contracts.MasterData;
using LeavePortal.Adapters.DataContext;
using LeavePortal.Adapters.Helpers;
using System.Data.Entity;

namespace LeavePortal.Adapters
{
    /// <summary>
    /// The MasterDataAdapter Class.
    /// </summary>
    /// <seealso cref="LeavePortal.Adapters.Contracts.IMasterDataAdapter" />
    public class MasterDataAdapter : IMasterDataAdapter
    {
        /// <summary>
        /// Gets the holiday.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A specific holiday.</returns>
        public HolidayModel GetHoliday(DateTime date)
        {
            var context = new LeavePortalEntities();
            var holiday = context.Holidays.Where(x => DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(date.Date)).SingleOrDefault();
            
            if (holiday == null)
            {
                return null;
            }

            var mapped = holiday.Map();

            return mapped;
        }

        /// <summary>
        /// Gets the leave types.
        /// </summary>
        /// <returns>
        /// A collection of leave types.
        /// </returns>
        public LeaveTypeCollection GetLeaveTypes()
        {
            var context = new LeavePortalEntities();
            var leaveTypes = context.LeaveTypes;
            
            if (leaveTypes == null)
            {
                return null;
            }

            var mapped = leaveTypes.Map();

            return new LeaveTypeCollection { Data = mapped };
        }
    }
}
