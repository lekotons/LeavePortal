using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeavePortal.Commons
{
    /// <summary>
    /// The DataResponse Class.
    /// </summary>
    public class DataResponse<T> : DataOperationOutcome
    {
        public T Data { get; set; }
    }
}
