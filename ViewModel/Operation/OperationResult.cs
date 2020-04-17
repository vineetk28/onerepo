using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MantiScanServices.ViewModel.Operation
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public string OperationData { get; set; }

        public string SuccessMessage { get; set; }

        public long ID { get; set; }

        public object ResultData { get; set; }
    }
}
