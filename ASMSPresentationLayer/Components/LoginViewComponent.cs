using ASMSPresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASMSPresentationLayer.Components
{
    public class LoginViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string email)
        {
            LoginViewModel model = new LoginViewModel()
            {
                Email = email
            };
            return View(model);
        }
    }
}
