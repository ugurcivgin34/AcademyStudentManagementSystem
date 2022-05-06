﻿using ASMSBusinessLayer.ViewModels;
using ASMSEntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMSBusinessLayer.EmailService
{
    public interface IEmailSender
    {
        Task SendMessage(EmailMessage message);
        
    }
}
