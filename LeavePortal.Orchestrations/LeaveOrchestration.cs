using LeavePortal.Orchestrations.Contracts.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeavePortal.Orchestrations.Contracts;
using LeavePortal.Adapters.Contracts;
using LeavePortal.Commons;

namespace LeavePortal.Orchestrations
{
    /// <summary>
    /// The LeaveOrchestration Class.
    /// </summary>
    /// <seealso cref="LeavePortal.Orchestrations.Contracts.Leave.ILeaveOrchestration" />
    public class LeaveOrchestration : ILeaveOrchestration
    {
        /// <summary>
        /// The leave adapter interface.
        /// </summary>
        private readonly ILeaveAdapter leaveAdapter;

        /// <summary>
        /// The employee adapter
        /// </summary>
        private readonly IEmployeeAdapter employeeAdapter;

        /// <summary>
        /// The master data
        /// </summary>
        private readonly IMasterDataAdapter masterData;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveOrchestration" /> class.
        /// </summary>
        /// <param name="employeeAdapter">The employee adapter.</param>
        /// <param name="leaveAdapter">The leave adapter.</param>
        /// <param name="masterData">The master data.</param>
        public LeaveOrchestration(IEmployeeAdapter employeeAdapter, ILeaveAdapter leaveAdapter, IMasterDataAdapter masterData)
        {
            this.employeeAdapter = employeeAdapter;
            this.leaveAdapter = leaveAdapter;
            this.masterData = masterData;
        }

        /// <summary>
        /// Approves the leave.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="approverId">The approver identifier.</param>
        /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
        /// <returns>The OperationOutcome.</returns>
        public OperationOutcome ApproveLeave(int employeeId, int approverId, bool isApproved)
        {
            var pendingLeaveOutcome = this.leaveAdapter.GetPendingLeave(employeeId);

            if (pendingLeaveOutcome == null)
            {
                return OperationOutcome.CreateError("Has not pending leave to approve");
            }

            var supervisorId = this.employeeAdapter.GetSupervisor(employeeId);

            if (supervisorId == null)
            {
                return OperationOutcome.CreateError("Employee not linked to a manager / supervisor");
            }

            if (supervisorId != approverId)
            {
                return OperationOutcome.CreateError("You are not authorised to approve the leave.");
            }

            var leaveBalanceOutcome = this.leaveAdapter.GetLeaveBalance(employeeId);

            if (leaveBalanceOutcome == null)
            {
                return OperationOutcome.CreateError("Unexpected internal error");
            }

            decimal latestBalance = 0;

            if (isApproved)
            {
                latestBalance = leaveBalanceOutcome.Balance - pendingLeaveOutcome.RequestedDays;
            }
            else
            {
                latestBalance = leaveBalanceOutcome.Balance;
            }

            var commitOutcome = this.leaveAdapter.CommitLeaveBalance(employeeId, latestBalance, isApproved);

            if (!commitOutcome.IsSuccessful)
            {
                return OperationOutcome.CreateError("Unable to commit new leave. ");
            }

            return OperationOutcome.Success;
        }

        /// <summary>
        /// Gets the pending leave.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// The LeaveRequest response.
        /// </returns>
        public Response<LeaveRequestModel> GetPendingLeave(int employeeId)
        {
            var pendingOutcome = this.leaveAdapter.GetPendingLeave(employeeId);

            if (pendingOutcome == null)
            {
                return new Response<LeaveRequestModel> { IsSuccessful = true, Data = null };
            }

            return new Response<LeaveRequestModel> { IsSuccessful = true, Data = pendingOutcome };
        }

        /// <summary>
        /// Gets the pending leave.
        /// </summary>
        /// <param name="approver">The approver.</param>
        /// <returns></returns>
        public Response<LeaveRequestCollection> GetPendingLeaveRequests(int approver)
        {
            var dataOutcome = this.leaveAdapter.GetPendingLeaveRequests(approver);

            if (dataOutcome == null)
            {
                return new Response<LeaveRequestCollection> { IsSuccessful = false };
            }

            return new Response<LeaveRequestCollection> { IsSuccessful = true, Data = dataOutcome };
        }

        /// <summary>
        /// Requests the leave.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="balance">The balance.</param>
        /// <param name="leaveTypeId">The leave type identifier.</param>
        /// <param name="comments">The comments.</param>
        /// <returns>The OperationOutcome.</returns>
        public OperationOutcome RequestLeave(int employeeId, DateTime fromDate, DateTime toDate, decimal balance, int leaveTypeId, string comments)
        {
            var employeeOutcome = this.employeeAdapter.GetEmployee(employeeId);

            if (!employeeOutcome.IsSuccessful)
            {
                return OperationOutcome.CreateError(employeeOutcome.ErrorMessage);
            }

            var hasPendingLeaveOutcome = this.leaveAdapter.HasPendingLeave(employeeId);

            if (hasPendingLeaveOutcome)
            {
                return OperationOutcome.CreateError(Errors.PendingLeave);
            }

            var hasAvailableLeaveOutcome = this.leaveAdapter.HasAvailableLeave(employeeId);

            if (!hasAvailableLeaveOutcome)
            {
                return OperationOutcome.CreateError(Errors.NoAvailableLeave);
            }

            var fromHolidayOutcome = this.masterData.GetHoliday(fromDate);

            if (fromHolidayOutcome != null)
            {
                return OperationOutcome.CreateError($"Leave request for {fromDate} fall on a holiday ({fromHolidayOutcome.Description})");
            }

            var toHolidayOutcome = this.masterData.GetHoliday(toDate);

            if (toHolidayOutcome != null)
            {
                return OperationOutcome.CreateError($"Leave request for {toDate} fall on a holiday ({toHolidayOutcome.Description})");
            }

            var isWeekendOutcome = this.IsWeekend(fromDate, toDate);

            if (isWeekendOutcome)
            {
                return OperationOutcome.CreateError("The date selected falls on a weekend.");
            }
            
            var numberofLeaveDaysRequested = this.GetNumberOfLeaveDaysRequested(fromDate, toDate);

            var supervisorId = this.employeeAdapter.GetSupervisor(employeeId);

            if (supervisorId == null)
            {
                return OperationOutcome.CreateError("Employee not linked to a manager / supervisor");
            }

            var addLeaveOutcome = this.leaveAdapter.AddNewLeaveRequest(employeeId, fromDate, toDate, numberofLeaveDaysRequested, balance, leaveTypeId, comments, supervisorId.Value);

            if (!addLeaveOutcome.IsSuccessful)
            {
                return OperationOutcome.CreateError(Errors.InternalError);
            }

            return OperationOutcome.Success;
        }

        /// <summary>
        /// Gets the leave balance.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public Response<LeaveBalanceModel> GetLeaveBalance(int employeeId)
        {
            var leaveBalanceOutcome = this.leaveAdapter.GetLeaveBalance(employeeId);

            if (leaveBalanceOutcome == null)
            {
                return new Response<LeaveBalanceModel> { IsSuccessful = false };
            }

            return new Response<LeaveBalanceModel> { IsSuccessful = true, Data = leaveBalanceOutcome };
        }

        /// <summary>
        /// Gets the number of leave days requested.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>An integer.</returns>
        private int GetNumberOfLeaveDaysRequested(DateTime startDate, DateTime endDate)
        {
            int days = 0;

            while (startDate < endDate)
            {
                if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++days;
                }

                startDate = startDate.AddDays(1);
            }

            return days;
        }

        /// <summary>
        /// Udpates the leave request.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="balance">The balance.</param>
        /// <param name="leaveTypeId">The leave type identifier.</param>
        /// <param name="comments">The comments.</param>
        /// <returns>
        /// The OperationOutcome.
        /// </returns>
        public OperationOutcome UdpateLeaveRequest(int employeeId, DateTime fromDate, DateTime toDate, decimal balance, int leaveTypeId, string comments)
        {
            var hasAvailableLeaveOutcome = this.leaveAdapter.HasAvailableLeave(employeeId);

            if (!hasAvailableLeaveOutcome)
            {
                return OperationOutcome.CreateError(Errors.NoAvailableLeave);
            }

            var fromHolidayOutcome = this.masterData.GetHoliday(fromDate);

            if (fromHolidayOutcome != null)
            {
                return OperationOutcome.CreateError($"Leave request for {fromDate} fall on a holiday ({fromHolidayOutcome.Description})"); // should look through all the days of requsted leave
            }

            var toHolidayOutcome = this.masterData.GetHoliday(toDate);

            if (toHolidayOutcome != null)
            {
                return OperationOutcome.CreateError($"Leave request for {toDate} fall on a holiday ({toHolidayOutcome.Description})"); 
            }

            var isWeekendOutcome = this.IsWeekend(fromDate, toDate);

            if (isWeekendOutcome)
            {
                return OperationOutcome.CreateError("The date selected falls on a weekend.");
            }

            var numberofLeaveDaysRequested = this.GetNumberOfLeaveDaysRequested(fromDate, toDate);

            var supervisorId = this.employeeAdapter.GetSupervisor(employeeId);

            if (supervisorId == null)
            {
                return OperationOutcome.CreateError("Employee not linked to a manager / supervisor");
            }

            var addLeaveOutcome = this.leaveAdapter.UpdateLeaveRequest(employeeId, fromDate, toDate, numberofLeaveDaysRequested, balance, leaveTypeId, comments, supervisorId.Value);

            if (!addLeaveOutcome.IsSuccessful)
            {
                return OperationOutcome.CreateError(Errors.InternalError);
            }

            return OperationOutcome.Success;
        }

        /// <summary>
        /// Determines whether the specified start date is weekend.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>True or False.</returns>
        private bool IsWeekend(DateTime startDate, DateTime endDate)
        {
            if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            if (endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the employee history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// The LeaveHistoryCollection.
        /// </returns>
        public Response<LeaveHistoryCollection> GetEmployeeHistory(int employeeId)
        {
            var outcome = this.leaveAdapter.GetEmployeeHistory(employeeId);

            if (outcome == null)
            {
                return new Response<LeaveHistoryCollection> { IsSuccessful = true };
            }

            return new Response<LeaveHistoryCollection> { IsSuccessful = true, Data = outcome };
        }

        /// <summary>
        /// Gets the manager history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// The LeaveHistoryCollection.
        /// </returns>
        public Response<LeaveHistoryCollection> GetManagerHistory(int employeeId)
        {
            var outcome = this.leaveAdapter.GetManagerHistory(employeeId);

            if (outcome == null)
            {
                return new Response<LeaveHistoryCollection> { IsSuccessful = true };
            }

            return new Response<LeaveHistoryCollection> { IsSuccessful = true, Data = outcome };
        }

        /// <summary>
        /// Cancels the leave.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public OperationOutcome CancelLeave(int employeeId)
        {
            var outcome = this.leaveAdapter.CancelLeave(employeeId);

            return OperationOutcome.Success;
        }
    }
}
