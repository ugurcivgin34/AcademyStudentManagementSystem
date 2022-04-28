using ASMSDataAccessLayer.ContractsDAL;
using ASMSEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSDataAccessLayer.İmplementationsDAL
{
    public class StudentAttendanceRepo :RepositoryBase<StudentAttendance,int>, IStudentAttendanceRepo
    {
        public StudentAttendanceRepo(MyContext myContext):base(myContext)
        {

        }
    }
}
