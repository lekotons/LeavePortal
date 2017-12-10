
namespace LeavePortal.Orchestrations.Contracts
{
    /// <summary>
    /// The Response Class.
    /// </summary>
    public class Response<T> : OperationOutcome
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; }
    }
}
