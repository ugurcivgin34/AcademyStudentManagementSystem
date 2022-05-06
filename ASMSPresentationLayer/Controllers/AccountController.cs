﻿using ASMSBusinessLayer.ContractBLL;
using ASMSBusinessLayer.EmailService;
using ASMSBusinessLayer.ViewModels;
using ASMSDataAccessLayer.ContractsDAL;
using ASMSEntityLayer.Enums;
using ASMSEntityLayer.IdentityModels;
using ASMSEntityLayer.ResultModels;
using ASMSEntityLayer.ViewModels;
using ASMSPresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ASMSPresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IStudentBusinessEngine _studentBusinessEngine;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IEmailSender emailSender, IStudentBusinessEngine studentBusinessEngine)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _studentBusinessEngine = studentBusinessEngine;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //Aynı emailden tekrar kayıt olunmasın
                var checkUserForEmail = await _userManager.FindByEmailAsync(model.Email);
                if (checkUserForEmail != null)
                {
                    ModelState.AddModelError("", "Bu email ile zaten sisteme kayıt yapılmıştır!");
                    return View(model);
                }
                //user'ı oluşturalım

                AppUser newUser = new AppUser()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    BirthDate = model.BirthDate.HasValue ? model.BirthDate.Value : null,
                    Gender = model.Gender,
                    EmailConfirmed = true,
                    UserName = model.Email
                };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)  //eklendi
                {
                    //rol ataması
                    var roleResult = await _userManager.AddToRoleAsync(newUser, ASMSRoles.Student.ToString());
                    if (roleResult.Succeeded == false)
                    {
                        //Admine gizliden bir email gönder eklensin rolü
                    }
                    //Student eklensin
                    StudentVM newStudent = new StudentVM()
                    {
                        UserId = newUser.Id,
                        TCNumber = model.TCNumber
                    };

                    IResult resultStudent = _studentBusinessEngine.Add(newStudent);
                    if (resultStudent.IsSuccess == false)
                    {
                        //Admine gizliden bir email gönder eklesin öğrenciyi
                    }
                    //email gönderilsin
                    var emailToStudent = new EmailMessage()
                    {
                        Subject = "ASMS Sistemine HOŞ GELDİNİZ!" + newUser.Name + " " + newUser.Surname,
                        Body = "Merhaba, Sisteme kaydınız gerçekleşmiştir...",
                        Contacts = new string[] { model.Email }
                    };
                    return RedirectToAction("Login", "Account", new { email = model.Email });
                }
                else
                {
                    ModelState.AddModelError("", "Beklenmedik bir sorun oldu.Üye kaydı başarısız tekrar deneyiniz!");
                    return View(model);
                }
            }
            catch (Exception)
            {
                //loglanacak
                return RedirectToAction("Error", "Home");
            }
        }


    }
}
