using LeavePortal.Orchestrations.Contracts.Leave;
using LeavePortal.Orchestrations.Contracts.MasterData;
using LeavePortal.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeavePortal.Web.Helpers
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
        /// <returns>A array of SelectListItem.</returns>
        public static SelectListItem[] Map(this IEnumerable<LeaveTypeModel> original)
        {
            var mappedOutcome = original.Select(x => x.Map());

            return mappedOutcome.ToArray();
        }

        /// <summary>
        /// Maps the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>The SelectListItem.</returns>
        public static SelectListItem Map (this LeaveTypeModel original)
        {
            var mappedOutcome = new SelectListItem
            {
                Text = original.Description,
                Value = original.LeaveTypeId.ToString()
            };

            return mappedOutcome;
        }

        /// <summary>
        /// Maps the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static LeaveBalanceViewModel Map(this LeaveBalanceModel original)
        {
            var mappedOutcome = new LeaveBalanceViewModel
            {
                EmployeeId = original.EmployeeId,
                Balance = original.Balance,
                Name = original.Name,
                Surname = original.Surname
            };

            return mappedOutcome;
        }

        public static PendingLeaveViewModel Map(this LeaveRequestModel original)
        {
            var mappedOutcome = new PendingLeaveViewModel
            {
               RequestedLeaveInDays = original.RequestedDays,
               LeaveBalanceInDays = original.Balance,
               Comments = original.Comments,
               FromDate = original.StartDate,
               ToDate = original.EndDate,
               RequestId = original.LeaveRequestId,
               EmployeeId = original.EmployeeId,
               LeaveType = original.LeaveType,
               DateRequested = original.DateRequested
            };

            return mappedOutcome;
        }

        /// <summary>
        /// Maps the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>The HistoryViewModel.</returns>
        public static HistoryViewModel Map(this LeaveHistoryModel original)
        {
            var mappedOutcome = new HistoryViewModel
            {
                RequestedLeaveInDays = original.RequestedDays,
                LeaveBalanceInDays = original.Balance,
                Comments = original.Comments,
                FromDate = original.StartDate,
                ToDate = original.EndDate,
                RequestId = original.LeaveRequestId,
                EmployeeId = original.EmployeeId,
                LeaveType = original.LeaveType,
                Cancelled = original.LeaveCancelled,
                DateRequested = original.DateRequested
            };

            return mappedOutcome;
        }

        /// <summary>
        /// Maps the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>A collection of HistoryViewModel.</returns>
        public static IEnumerable<HistoryViewModel> Map(this IEnumerable<LeaveHistoryModel> data)
        {
            var mappedOutcome = data.Select(x => x.Map());

            return mappedOutcome;
        }

        public static LeaveCreationViewModel MapEdit(this LeaveRequestModel original)
        {
            var mappedOutcome = new LeaveCreationViewModel
            {
                LeaveBalanceInDays = original.Balance,
                Comments = original.Comments,
                FromDate = original.StartDate,
                ToDate = original.EndDate,
                Balance = original.Balance,
                LeaveRequestedInDays = original.RequestedDays,
                LeaveTypeId = original.LeaveRequestId
            };

            return mappedOutcome;
        }

        public static IEnumerable<PendingLeaveViewModel> Map(this IEnumerable<LeaveRequestModel> data)
        {
            var mappedOutcome = data.Select(x => x.Map());

            return mappedOutcome;
        }
    }
}