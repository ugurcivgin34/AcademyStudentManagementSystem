using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.ResultModels
{
    public class Result : IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; }
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        public Result(bool success)
        {
            IsSuccess = success;
        }




    }
}
