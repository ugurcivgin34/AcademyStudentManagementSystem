﻿using ASMSEntityLayer.Enums;
using ASMSEntityLayer.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System;

namespace ASMSPresentationLayer.CreateDefaultData
{
    public static class CreateData
    {
        public static void Create(RoleManager<AppRole> roleManager)
        {
            CheckAndCreateRoles(roleManager);
        }

        private static  void CheckAndCreateRoles(RoleManager<AppRole> roleManager)
        {
            var allRoles = Enum.GetNames(typeof(ASMSRoles));
            foreach (var item in allRoles)
            {
                if (!roleManager.RoleExistsAsync(item).Result)
                {

                    AppRole role = new AppRole()
                    {
                        Name = item,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        Description = $"Sistem tarafından {item} rolü eklendi."
                    };
                    roleManager.CreateAsync(role);


                }
            }
        }
    }
}
