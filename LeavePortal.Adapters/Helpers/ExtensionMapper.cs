using LeavePortal.Adapters.DataContext;
using LeavePortal.Orchestrations.Contracts.Employees;
using LeavePortal.Orchestrations.Contracts.Leave;
using LeavePortal.Orchestrations.Contracts.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Adapters.Helpers
{
    /// <summary>
    /// The ExtensionMapper Class.
    /// </summary>
    public static class ExtensionMapper
    {
        /// <summary>
        /// Maps the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static IEnumerable<LeaveTypeModel> Map(this IEnumerable<LeaveType> original)
        {
            var mappedOutcome = original.Select(x => x.Map());

            return mappedOutcome;
        }

        /// <summary>
        /// Maps the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static LeaveTypeModel Map(this LeaveType original)
        {
            var mappedOutcome = new LeaveTypeModel
            {
                LeaveTypeId = original.LeaveTypeId,
                Description = original.Description
            };

            return mappedOutcome;
        }

        /// <summary>
        /// Maps the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static HolidayModel Map(this Holiday original)
        {
            var mappedOutcome = new HolidayModel
            {
                Description = original.Description
            };

            return mappedOutcome;
        }

        /// <summary>
        /// Maps the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static LeaveBalanceModel Map(this LeaveBalance original, string name, string surname)
        {
            var mappedOutcome = new LeaveBalanceModel
            {
                EmployeeId = original.EmployeeId.Value,
                Balance = original.Balance.Value,
                Name = name,
                Surname = surname
            };

            return mappedOutcome;
        }

        /// <summary>
        /// Maps the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static LeaveRequestModel Map(this LeaveRequest original)
        {
            var mappedOutcome = new LeaveRequestModel
            {
                LeaveRequestId = original.LeaveRequestId,
                Balance = original.RemainingLeaveBalance,
                Comments = original.Comments,
                RequestedDays = original.LeaveRequestedInDays,
                EndDate = original.ToDate,
                StartDate = original.FromDate,
                EmployeeId = original.EmployeeId, 
                LeaveType = original.LeaveType.Description,
                DateRequested = original.DateRequested
            };

            return mappedOutcome;
        }

        /// <summary>
        /// Maps the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static LeaveRequestCollection Map(this IEnumerable<LeaveRequest> data)
        {
            var mappedOutcome = data.Select(x => x.Map());

            return new LeaveRequestCollection { Requests = mappedOutcome };
        }

        /// <summary>
        /// Maps the history.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The LeaveHistoryCollection.</returns>
        public static LeaveHistoryCollection MapHistory(this IEnumerable<LeaveRequest> data)
        {
            var mappedOutcome = data.Select(x => x.MapHistory());

            return new LeaveHistoryCollection { History = mappedOutcome };
        }

        /// <summary>
        /// Maps the history.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>The LeaveHistoryModel.</returns>
        public static LeaveHistoryModel MapHistory(this LeaveRequest original)
        {
            var mappedOutcome = new LeaveHistoryModel
            {
                LeaveRequestId = original.LeaveRequestId,
                Balance = original.RemainingLeaveBalance,
                Comments = original.Comments,
                RequestedDays = original.LeaveRequestedInDays,
                EndDate = original.ToDate,
                StartDate = original.FromDate,
                EmployeeId = original.EmployeeId,
                LeaveType = original.LeaveType.Description,
                LeaveCancelled = original.LeaveCancelled,
                DateRequested = original.DateRequested
            };

            return mappedOutcome;
        }

        /// <summary>
        /// Maps the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static EmployeeModel Map(this Employee original)
        {
            var mappedOutcome = new EmployeeModel
            {
                EmployeeId = original.EmployeeID,
                FirstNames = original.Name,
                Surname = original.Surname
            };

            return mappedOutcome;
        }

        public static EmployeeModel Map(this AspNetUser original)
        {
            var mappedOutcome = new EmployeeModel
            {
                EmployeeId = Convert.ToInt32(original.EmployeeId),
                FirstNames = original.Name,
                Surname = original.Surname
            };

            return mappedOutcome;
        }
    }
}
