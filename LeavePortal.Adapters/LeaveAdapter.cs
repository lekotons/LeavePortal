using LeavePortal.Adapters.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeavePortal.Commons;
using LeavePortal.Orchestrations.Contracts.Leave;
using LeavePortal.Adapters.DataContext;
using LeavePortal.Adapters.Helpers;

namespace LeavePortal.Adapters
{
    public class LeaveAdapter : ILeaveAdapter
    {
        /// <summary>
        /// Adds the new leave request.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="daysRequested">The days requested.</param>
        /// <param name="remainingBalance">The remaining balance.</param>
        /// <param name="leaveTypeId">The leave type identifier.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="supervisorId">The supervisor identifier.</param>
        /// <returns>
        /// The DataOperationOutcome.
        /// </returns>
        public DataOperationOutcome AddNewLeaveRequest(int employeeId, DateTime fromDate, DateTime toDate, decimal daysRequested, decimal remainingBalance, int leaveTypeId, string comments, int supervisorId)
        {
            var context = new LeavePortalEntities();
            var addleaveRequest = new LeaveRequest
            {
                FromDate = fromDate,
                ToDate = toDate,
                LeaveRequestedInDays = daysRequested,
                RemainingLeaveBalance = remainingBalance,
                IsPending = true,
                EmployeeId = employeeId,
                ApprovedBy = supervisorId,
                LeaveTypeId = leaveTypeId,
                Comments = comments,
                LeaveCancelled = false,
                DateRequested = DateTime.Now
            };

            context.LeaveRequests.Add(addleaveRequest);
            context.SaveChanges();

            return DataOperationOutcome.Success;
        }

        /// <summary>
        /// Updates the leave request.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="daysRequested">The days requested.</param>
        /// <param name="remainingBalance">The remaining balance.</param>
        /// <param name="leaveTypeId">The leave type identifier.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="supervisorId">The supervisor identifier.</param>
        /// <returns>The DataOperationOutcome</returns>
        public DataOperationOutcome UpdateLeaveRequest(int employeeId, DateTime fromDate, DateTime toDate, decimal daysRequested, decimal remainingBalance, int leaveTypeId, string comments, int supervisorId)
        {
            var context = new LeavePortalEntities();
            var leaveRequest = context.LeaveRequests.SingleOrDefault(x => x.EmployeeId == employeeId && x.IsPending == true);

            leaveRequest.FromDate = fromDate;
            leaveRequest.ToDate = toDate;
            leaveRequest.LeaveRequestedInDays = daysRequested;
            leaveRequest.RemainingLeaveBalance = remainingBalance;
            leaveRequest.LeaveTypeId = leaveTypeId;
            leaveRequest.Comments = comments;

            context.SaveChanges();

            return DataOperationOutcome.Success;
        }

        /// <summary>
        /// Commits the leave balance.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="leaveBalance">The leave balance.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns>
        /// Teh DataOperationOutcome.
        /// </returns>
        public DataOperationOutcome CommitLeaveBalance(int employeeId, decimal leaveBalance, bool approved)
        {
            var context = new LeavePortalEntities();
            var leaveBalanceOutcome = context.LeaveBalances.SingleOrDefault(x => x.EmployeeId == employeeId);
            var leaveRequestOutcome = context.LeaveRequests.SingleOrDefault(x => x.EmployeeId == employeeId && x.IsPending == true);

            leaveBalanceOutcome.Balance = leaveBalance;
            leaveRequestOutcome.IsApproved = approved;
            leaveRequestOutcome.IsPending = false;
            context.SaveChanges();

            return DataOperationOutcome.Success;
        }

        /// <summary>
        /// Gets the leave balance.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// The leave balance.
        /// </returns>
        public LeaveBalanceModel GetLeaveBalance(int employeeId)
        {
            var context = new LeavePortalEntities();
            var leaveBalance = context.LeaveBalances.SingleOrDefault(x => x.EmployeeId == employeeId);
            var employee = context.AspNetUsers.SingleOrDefault(x => x.EmployeeId == employeeId.ToString());

            if (leaveBalance == null)
            {
                return null;
            }

            var mapped = leaveBalance.Map(employee.Name, employee.Surname);

            return mapped;
        }

        /// <summary>
        /// Gets the pending leave.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// Leave request.
        /// </returns>
        public LeaveRequestModel GetPendingLeave(int employeeId)
        {
            var context = new LeavePortalEntities();
            var leaveRequest = context.LeaveRequests.Where(x => x.EmployeeId == employeeId && x.IsPending == true).FirstOrDefault();

            if (leaveRequest == null)
            {
                return null;
            }

            var mapped = leaveRequest.Map();

            return mapped;
        }

        /// <summary>
        /// Gets the pending leave requests.
        /// </summary>
        /// <param name="approver">The approver.</param>
        /// <returns></returns>
        public LeaveRequestCollection GetPendingLeaveRequests(int approver)
        {
            var context = new LeavePortalEntities();
            var leaveRequest = context.LeaveRequests.Where(x => x.ApprovedBy == approver && x.IsPending == true).OrderBy(x => x.EmployeeId);

            if (leaveRequest == null)
            {
                return null;
            }

            var mapped = leaveRequest.Map();

            return mapped;
        }

        /// <summary>
        /// Determines whether [has available leave] [the specified employee identifier].
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// The bool DataResponse.
        /// </returns>
        public bool HasAvailableLeave(int employeeId)
        {
            var context = new LeavePortalEntities();
            var leaveBalance = context.LeaveBalances.Any(x => x.EmployeeId == employeeId && x.Balance > 0);

            return leaveBalance;
        }

        /// <summary>
        /// Determines whether [has pending leave] [the specified employee identifier].
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// The bool DataResponse.
        /// </returns>
        public bool HasPendingLeave(int employeeId)
        {
            var context = new LeavePortalEntities();
            var pendingLeave = context.LeaveRequests.Where(x => x.EmployeeId == employeeId && x.IsPending == true).Any();

            return pendingLeave;
        }

        /// <summary>
        /// Gets the employee history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The LeaveHistoryCollection.</returns>
        public LeaveHistoryCollection GetEmployeeHistory(int employeeId)
        {
            var context = new LeavePortalEntities();
            var pendingLeaveOutcome = context.LeaveRequests.Where(x => x.EmployeeId == employeeId && x.IsPending == false).OrderBy(x => x.EmployeeId);

            if (pendingLeaveOutcome.Count() == 0)
            {
                return null;
            }

            var mappedHistory = pendingLeaveOutcome.MapHistory();

            return mappedHistory;
        }

        /// <summary>
        /// Gets the manager history.
        /// </summary>
        /// <param name="approverId">The approver identifier.</param>
        /// <returns>The LeaveHistoryCollection.</returns>
        public LeaveHistoryCollection GetManagerHistory(int approverId)
        {
            var context = new LeavePortalEntities();
            var pendingLeaveOutcome = context.LeaveRequests.Where(x => x.ApprovedBy == approverId && x.IsApproved != null);

            if (pendingLeaveOutcome.Count() == 0)
            {
                return null;
            }

            var mappedHistory = pendingLeaveOutcome.MapHistory();

            return mappedHistory;
        }

        /// <summary>
        /// Cancels the leave.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returnsThe Outcome.</returns>
        public DataOperationOutcome CancelLeave(int employeeId)
        {
            var context = new LeavePortalEntities();
            var leaveRequestOutcome = context.LeaveRequests.SingleOrDefault(x => x.EmployeeId == employeeId && x.IsPending == true);

            leaveRequestOutcome.IsPending = false;
            leaveRequestOutcome.LeaveCancelled = true;
            context.SaveChanges();

            return DataOperationOutcome.Success;
        }
    }
}
