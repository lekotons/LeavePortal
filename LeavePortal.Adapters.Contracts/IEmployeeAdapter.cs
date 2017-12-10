using LeavePortal.Commons;
using LeavePortal.Orchestrations.Contracts.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Adapters.Contracts
{
    /// <summary>
    /// The IEmployeeAdapter interface.
    /// </summary>
    public interface IEmployeeAdapter
    {
        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The Employee DataResponse.</returns>
        DataResponse<EmployeeModel> GetEmployee(int employeeId);

        /// <summary>
        /// Gets the supervisor.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The supervisor id.</returns>
        int? GetSupervisor(int employeeId);
    }
}
