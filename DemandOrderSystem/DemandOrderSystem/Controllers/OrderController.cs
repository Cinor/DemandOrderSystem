///需求單表Controller
///表一 TableOne
///表二 TableTwo 以此類推
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemandOrderSystem.Library;
using DemandOrderSystem.Models.ViewModel;

namespace DemandOrderSystem.Controllers
{
    public class OrderController : Controller
    {
        OrderLibrary dbLibrary = new OrderLibrary();


        /// <summary>
        /// 需求單受理狀態表
        /// </summary>
        /// <returns></returns>
        public ActionResult TableOne()
        {
            TableOneViewModel viewModel = new TableOneViewModel
            {
                Orders = dbLibrary.getOrderDatas()
            };

            return View(viewModel);
        }
    }
}