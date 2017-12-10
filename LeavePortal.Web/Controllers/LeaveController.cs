using LeavePortal.Commons;
using LeavePortal.Orchestrations.Contracts.Leave;
using LeavePortal.Orchestrations.Contracts.MasterData;
using LeavePortal.Web.Helpers;
using LeavePortal.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeavePortal.Web.Controllers
{

    /// <summary>
    /// The LeaveController Class.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class LeaveController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>A dashboard view model.</returns>
        public ActionResult Index()
        {
            var employeeId = CurrentLoggedInUser.EmployeeId;

            var leaveOrchestration = Factory.CreateInstance<ILeaveOrchestration>();
            var balanceOutcome = leaveOrchestration.GetLeaveBalance(employeeId);

            if (!balanceOutcome.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, balanceOutcome.ErrorMessage);
                return View(new DashboardViewModel());
            }

            var pendingOutcome = leaveOrchestration.GetPendingLeave(employeeId);

            if (!pendingOutcome.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, pendingOutcome.ErrorMessage);
                return View(new DashboardViewModel());
            }

            var historyOutcome = leaveOrchestration.GetEmployeeHistory(employeeId);

            var viewModel = new DashboardViewModel
            {
                BalanceModel = balanceOutcome.Data == null ? null : balanceOutcome.Data.Map(),
                PendingModel = pendingOutcome.Data == null ? null : pendingOutcome.Data.Map(),
                HistoryModel = historyOutcome.Data == null ? null : historyOutcome.Data.History.Map()
            };

            return View(viewModel);
        }

        // GET: Leave/Create
        public ActionResult Create()
        {
            var employeeId = CurrentLoggedInUser.EmployeeId;

            var leaveOrchestration = Factory.CreateInstance<ILeaveOrchestration>();
            var masterDataOrchestration = Factory.CreateInstance<IMasterDataOrchestration>();

            var leaveTypes = masterDataOrchestration.GetLeaveTypes();
            var leaveBalanceOutcome = leaveOrchestration.GetLeaveBalance(employeeId);

            if (!leaveBalanceOutcome.IsSuccessful)
            {

            }

            return View(new LeaveCreationViewModel { LeaveTypes = leaveTypes.Data.Map(), LeaveBalanceInDays = leaveBalanceOutcome.Data.Balance });
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A created model.</returns>
        [HttpPost]
        public ActionResult Create(LeaveCreationViewModel model)
        {
            try
            {
                var employeeId = CurrentLoggedInUser.EmployeeId;
                var leaveOrchestration = Factory.CreateInstance<ILeaveOrchestration>();
                var outcome = leaveOrchestration.RequestLeave(employeeId, model.FromDate, model.ToDate, model.LeaveBalanceInDays, model.LeaveTypeId, model.Comments);

                if (!outcome.IsSuccessful)
                {
                    var masterDataOrchestration = Factory.CreateInstance<IMasterDataOrchestration>();
                    var leaveTypes = masterDataOrchestration.GetLeaveTypes();
                    model.LeaveTypes = leaveTypes.Data.Map();

                    ModelState.AddModelError(string.Empty, outcome.ErrorMessage);

                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Pendings the leave.
        /// </summary>
        /// <returns>A collection of requests.</returns>
        [Authorize(Roles = "Manager")]
        public ActionResult PendingLeave()
        {
            var currentUserId = CurrentLoggedInUser.EmployeeId;
            var leaveOrchestration = Factory.CreateInstance<ILeaveOrchestration>();
            var masterDataOrchestration = Factory.CreateInstance<IMasterDataOrchestration>();

            var outcome = leaveOrchestration.GetPendingLeaveRequests(currentUserId);
            var historyOutcome = leaveOrchestration.GetManagerHistory(currentUserId);

            if (!outcome.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, outcome.ErrorMessage);
            }

            var viewModel = new AwaitingApprovalViewModel
            {
                Requests = outcome.Data == null ? null : outcome.Data.Requests.Map(),
                History = historyOutcome.Data == null ? null : historyOutcome.Data.History.Map()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Approves the or decline leave request.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns>The PendingLeave view.</returns>
        public ActionResult ApproveOrDeclineLeaveRequest(int requestId, int employeeId, bool approved)
        {
            var currentUserId = CurrentLoggedInUser.EmployeeId;

            var leaveOrchestration = Factory.CreateInstance<ILeaveOrchestration>();
            var requestOutcome = leaveOrchestration.GetPendingLeaveRequests(currentUserId);
            var historyOutcome = leaveOrchestration.GetManagerHistory(currentUserId);
            var viewModel = new AwaitingApprovalViewModel
            {
                Requests = requestOutcome.Data == null ? null : requestOutcome.Data.Requests.Map(),
                History = historyOutcome.Data == null ? null : historyOutcome.Data.History.Map()
            };

            var outcome = leaveOrchestration.ApproveLeave(employeeId, currentUserId, approved);

            if (!outcome.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, outcome.ErrorMessage);
            }

            return View("PendingLeave", viewModel);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An edit model.</returns>
        public ActionResult Edit(int id)
        {
            var leaveOrchestration = Factory.CreateInstance<ILeaveOrchestration>();
            var masterDataOrchestration = Factory.CreateInstance<IMasterDataOrchestration>();

            var outcome = leaveOrchestration.GetPendingLeave(id);

            if (!outcome.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, outcome.ErrorMessage);
            }

            var leaveTypes = masterDataOrchestration.GetLeaveTypes();
            var mappedOutcome = outcome.Data == null ? null : outcome.Data.MapEdit();
            mappedOutcome.LeaveTypes = leaveTypes.Data.Map();

            return View(mappedOutcome);
        }

        // POST: Leave/Edit/5
        [HttpPost]
        public ActionResult Edit(LeaveCreationViewModel updateModel)
        {
            try
            {
                var employeeId = CurrentLoggedInUser.EmployeeId;
                var leaveOrchestration = Factory.CreateInstance<ILeaveOrchestration>();
                var outcome = leaveOrchestration.UdpateLeaveRequest(employeeId, updateModel.FromDate, updateModel.ToDate, updateModel.LeaveBalanceInDays, updateModel.LeaveTypeId, updateModel.Comments);

                if (!outcome.IsSuccessful)
                {
                    var masterDataOrchestration = Factory.CreateInstance<IMasterDataOrchestration>();
                    var leaveTypes = masterDataOrchestration.GetLeaveTypes();
                    updateModel.LeaveTypes = leaveTypes.Data.Map();

                    ModelState.AddModelError(string.Empty, outcome.ErrorMessage);

                    return View(updateModel);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CancelLeave(int employeeId)
        {
            var leaveOrchestration = Factory.CreateInstance<ILeaveOrchestration>();
            var outcome = leaveOrchestration.CancelLeave(employeeId);

            return RedirectToAction("Index");

        }
    }
}
