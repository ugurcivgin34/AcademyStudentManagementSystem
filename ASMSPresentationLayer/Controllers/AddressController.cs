﻿using ASMSBusinessLayer.ContractBLL;
using ASMSBusinessLayer.EmailService;
using ASMSEntityLayer.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASMSPresentationLayer.Controllers
{

    [Authorize]
    public class AddressController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IUsersAddressBusinessEngine _userAdress;
        private readonly ICityBusinessEngine _cityEngine;

        public AddressController(UserManager<AppUser> userManager, IEmailSender emailSender, IUsersAddressBusinessEngine userAdress
            ,ICityBusinessEngine cityEngine)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _userAdress = userAdress;
            _cityEngine = cityEngine;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddAddress()
        {
            //İlleri sayfaya götürsün
            ViewBag.Cities = _cityEngine.GetAll().Data;
            return View();
        }
    }
}
