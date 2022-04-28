using ASMSDataAccessLayer.ContractsDAL;
using ASMSEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSDataAccessLayer.İmplementationsDAL
{
    public class CourseGroupRepo:RepositoryBase<CourseGroup,int>,ICourseGroupRepo
    {
        public CourseGroupRepo(MyContext myContext):base(myContext)
        {

        }
    }
}
