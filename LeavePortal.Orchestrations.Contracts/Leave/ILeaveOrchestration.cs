using System;
using System.Linq;

namespace LeavePortal.Orchestrations.Contracts.Leave
{
    /// <summary>
    /// The ILeaveOrchestration interface.
    /// </summary>
    public interface ILeaveOrchestration
    {
        /// <summary>
        /// Requests the leave.
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
        OperationOutcome RequestLeave(int employeeId, DateTime fromDate, DateTime toDate, decimal balance, int leaveTypeId, string comments);

        /// <summary>
        /// Udpates the leave request.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="balance">The balance.</param>
        /// <param name="leaveTypeId">The leave type identifier.</param>
        /// <param name="comments">The comments.</param>
        /// <returns>The OperationOutcome.</returns>
        OperationOutcome UdpateLeaveRequest(int employeeId, DateTime fromDate, DateTime toDate, decimal balance, int leaveTypeId, string comments);

        /// <summary>
        /// Approves the leave.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <param name="approverId">The approver identifier.</param>
        /// <returns>The OperationOutcome.</returns>
        OperationOutcome ApproveLeave(int employeeId, int approverId, bool isApproved);

        /// <summary>
        /// Gets the pending leave.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The LeaveRequest response.</returns>
        Response<LeaveRequestModel> GetPendingLeave(int employeeId);

        /// <summary>
        /// Gets the pending leave.
        /// </summary>
        /// <param name="approver">The approver.</param>
        /// <returns></returns>
        Response<LeaveRequestCollection> GetPendingLeaveRequests(int approver);

        /// <summary>
        /// Gets the leave balance.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Response<LeaveBalanceModel> GetLeaveBalance(int employeeId);


        /// <summary>
        /// Gets the employee history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The LeaveHistoryCollection.</returns>
        Response<LeaveHistoryCollection> GetEmployeeHistory(int employeeId);

        /// <summary>
        /// Gets the manager history.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The LeaveHistoryCollection.</returns>
        Response<LeaveHistoryCollection> GetManagerHistory(int employeeId);

        /// <summary>
        /// Cancels the leave.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The OperationOutcome.</returns>
        OperationOutcome CancelLeave(int employeeId);
    }
}
