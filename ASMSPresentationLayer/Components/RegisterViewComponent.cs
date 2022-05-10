using ASMSPresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASMSPresentationLayer.Components
{
    public class RegisterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new RegisterViewModel());  
        }
    }
}
