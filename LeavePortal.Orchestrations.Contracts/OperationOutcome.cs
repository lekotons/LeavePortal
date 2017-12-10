
namespace LeavePortal.Orchestrations.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using LeavePortal.Commons;

    /// <summary>
    /// The OperationOutcome Class.
    /// </summary>
    public class OperationOutcome
    {
        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        public static OperationOutcome Success
        {
            get
            {
                return new OperationOutcome
                {
                    IsSuccessful = true
                };
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Creates the error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The OperationOutcome.</returns>
        public static OperationOutcome CreateError(string errorMessage)
        {
            return new OperationOutcome
            {
                IsSuccessful = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
