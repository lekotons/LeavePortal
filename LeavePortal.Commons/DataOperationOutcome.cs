
namespace LeavePortal.Commons
{
    using System;

    public class DataOperationOutcome
    {
        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        public static DataOperationOutcome Success
        {
            get
            {
                return new DataOperationOutcome
                {
                    IsSuccessful = true
                };
            }
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Creates the error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The DataOperationOutcome.</returns>
        public DataOperationOutcome CreateError(String errorMessage)
        {
            return new DataOperationOutcome
            {
                IsSuccessful = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
