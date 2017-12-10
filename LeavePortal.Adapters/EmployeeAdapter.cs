using LeavePortal.Adapters.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeavePortal.Commons;
using LeavePortal.Orchestrations.Contracts.Employees;
using LeavePortal.Adapters.DataContext;
using LeavePortal.Adapters.Helpers;

namespace LeavePortal.Adapters
{
    /// <summary>
    /// The EmployeeAdapter Class.
    /// </summary>
    /// <seealso cref="LeavePortal.Adapters.Contracts.IEmployeeAdapter" />
    public class EmployeeAdapter : IEmployeeAdapter
    {
        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// The Employee DataResponse.
        /// </returns>
        public DataResponse<EmployeeModel> GetEmployee(int employeeId)
        {
            var context = new LeavePortalEntities();
            //var employee = context.Employees.Where(x => x.EmployeeID == employeeId).SingleOrDefault();
            var employee = context.AspNetUsers.Where(x => x.EmployeeId == employeeId.ToString()).SingleOrDefault();

            if (employee == null)
            {
                return new DataResponse<EmployeeModel> { IsSuccessful = false, ErrorMessage = "Unable to find the employee id" };
            }

            var mapped = employee.Map();

            return new DataResponse<EmployeeModel> { IsSuccessful = true, Data = mapped };
        }

        /// <summary>
        /// Gets the supervisor.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>
        /// The supervisor id.
        /// </returns>
        public int? GetSupervisor(int employeeId)
        {
            var context = new LeavePortalEntities();
            var supervisor = context.Supervisors.SingleOrDefault(x => x.EmployeeId == employeeId);

            if (supervisor == null)
            {
                return null;
            }

            return supervisor.Supervisor1;
        }
    }
}
