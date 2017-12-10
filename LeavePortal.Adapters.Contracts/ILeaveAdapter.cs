using LeavePortal.Commons;
using LeavePortal.Orchestrations.Contracts.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Adapters.Contracts
{
    /// <summary>
    /// The ILeaveAdapter interface.
    /// </summary>
    public interface ILeaveAdapter
    {
        /// <summary>
        /// Adds the new leave request.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="daysRequested">The days requested.</param>
        /// <param name="remaingBalance">The remaing balance.</param>
        /// <param name="leaveTypeId">The leave type identifier.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="supervisorId">The supervisor identifier.</param>
        /// <returns>
        /// The DataOperationOutcome.
        /// </returns>
        DataOperationOutcome AddNewLeaveRequest(int employeeId, DateTime fromDate, DateTime toDate, decimal daysRequested, decimal remaingBalance, int leaveTypeId, string comments, int supervisorId);

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
        /// <returns>The DataOperationOutcome.</returns>
        DataOperationOutcome UpdateLeaveRequest(int employeeId, DateTime fromDate, DateTime toDate, decimal daysRequested, decimal remainingBalance, int leaveTypeId, string comments, int supervisorId);

        /// <summary>
        /// Determines whether [has pending leave] [the specified employee identifier].
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The bool DataResponse.</returns>
        bool HasPendingLeave(int employeeId);

        /// <summary>
        /// Determines whether [has available leave] [the specified employee identifier].
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The bool DataResponse.</returns>
        bool HasAvailableLeave(int employeeId);

        /// <summary>
        /// Gets the pending leave.
        /// </summary>
        /// <param name="eomployeeId">The eomployee identifier.</param>
        /// <returns>Leave request.</returns>
        LeaveRequestModel GetPendingLeave(int eomployeeId);

        /// <summary>
        /// Gets the pending leave requests.
        /// </summary>
        /// <param name="eomployeeId">The eomployee identifier.</param>
        /// <returns></returns>
        LeaveRequestCollection GetPendingLeaveRequests(int approver);

        /// <summary>
        /// Gets the leave balance.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// The leave balance.
        /// </returns>
        LeaveBalanceModel GetLeaveBalance(int employeeId);

        /// <summary>
        /// Gets the employee history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        LeaveHistoryCollection GetEmployeeHistory(int employeeId);

        /// <summary>
        /// Gets the manager history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        LeaveHistoryCollection GetManagerHistory(int employeeId);

        /// <summary>
        /// Cancels the leave.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        DataOperationOutcome CancelLeave(int employeeId);

        /// <summary>
        /// Commits the leave balance.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="leaveBalance">The leave balance.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns>
        /// Teh DataOperationOutcome.
        /// </returns>
        DataOperationOutcome CommitLeaveBalance(int employeeId, decimal leaveBalance, bool approved);
    }
}
