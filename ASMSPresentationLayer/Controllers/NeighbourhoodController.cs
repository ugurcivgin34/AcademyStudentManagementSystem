using ASMSBusinessLayer.ContractBLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASMSPresentationLayer.Controllers
{
    public class NeighbourhoodController : Controller
    {
        private readonly INeighbourhoodBusinessEngine _neighbourhoodBusinessEngine;

        public NeighbourhoodController(INeighbourhoodBusinessEngine neighbourhoodBusinessEngine)
        {
            _neighbourhoodBusinessEngine = neighbourhoodBusinessEngine;
        }

        public JsonResult GetDistrictNeighbourhoods(byte id)
        {
            try
            {
                var data = _neighbourhoodBusinessEngine.GetNeighbourhoodsOfDistrict(id).Data;
                return Json(new { isSuccess = true, data });
            }
            catch (Exception)
            {
                //ex loglanabilir
                return Json(new { isSuccess = false });
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
