using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEntities
{
    public class ProcessResult
    {
        /// <summary>
        /// Results of the process, true if the process finished correctly or false if the process finished in a wrong way
        /// </summary>
        public bool ProcessResults { get; set; }

        /// <summary>
        ///  Message of the process
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Message detailed if it's necessary
        /// </summary>
        public string MessageDetails { get; set; }

        /// <summary>
        /// Error code if it's necesary
        /// </summary>
        public string ErrorCode { get; set; }
    }
}
