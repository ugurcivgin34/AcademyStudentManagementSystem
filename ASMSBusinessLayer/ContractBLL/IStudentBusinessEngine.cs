using ASMSEntityLayer.ResultModels;
using ASMSEntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSBusinessLayer.ContractBLL
{
    public interface IStudentBusinessEngine
    {
        IResult Add(StudentVM student);
    }
}
