﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSEntityLayer.ResultModels
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message):base(true,message)
        {


        }
        public SuccessResult():base(true) //public SuccessResult(bool success=true):base(success)
        {

        }
    }
}
