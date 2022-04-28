﻿using ASMSDataAccessLayer.ContractsDAL;
using ASMSEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSDataAccessLayer.İmplementationsDAL
{
    public class CityRepo : RepositoryBase<City,byte>, ICityRepo
    {
        //ICityRepo kalıtım almamızın sebebi DI yapısına uygun çalışmak
        //RepositoryBase kalıtım almamızın sebebi içindeki CRUD metotlarını kullanmak
        public CityRepo(MyContext myContext):base(myContext)
        {
            //ctor oluşturmamızın sebebi kalıtım aldığımız class'ın ctor'ında myContext istendiği için
        }
    }
}
