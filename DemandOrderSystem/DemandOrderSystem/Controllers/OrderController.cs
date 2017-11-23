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

        /// <summary>
        /// 回傳需求單總表
        /// </summary>
        /// <returns></returns>
        public ActionResult TableAll(String applicant_department_list, String information_management_list, DateTime? evaluate_recived_date_start, DateTime? evaluate_recived_date_end, DateTime? closed_date_start, DateTime? closed_date_end, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var tableAllViewModel = dbLibrary.GetTableAllViewModel(applicant_department_list,information_management_list,evaluate_recived_date_start,evaluate_recived_date_end,closed_date_start,closed_date_end,currentPage);
            return View(tableAllViewModel);
        }

        /// <summary>
        /// 回傳需求單總表Excel
        /// </summary>
        /// <returns></returns>
        public ActionResult TableAllExcelDownload()
        {
            return this.File(dbLibrary.GetTableAllExcelData().ToArray(), "application/vnd.ms-excel", "需求單總表.xlsx");
        }

        /// <summary>
        /// 回傳需求單分類統計表
        /// </summary>
        /// <returns></returns>
        public ActionResult TableFour(TableFourViewModel model)
        {
            TableFourViewModel classificationViewModel = new TableFourViewModel();
            if (model.startTime == new DateTime())
            {
                classificationViewModel.countByMonth = new List<CountByMonth>();
                return View(classificationViewModel);
            }
            else
            {
                classificationViewModel = dbLibrary.GetTableFourViewModel(model.startTime, model.endTime);
                classificationViewModel.startTime = model.startTime;
                classificationViewModel.endTime = model.endTime;
                return View(classificationViewModel);
            }
            
        }

        /// <summary>
        /// 回傳需求單未結案且未回覆預估完成日件數統計及明細表
        /// </summary>
        /// <param name="condition">需求單狀態選擇</param>
        /// <returns></returns>
        public ActionResult TableFive(String condition)
        {
            TableFiveViewModel result = dbLibrary.GetTableFiveViewModel(condition);


            return View(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public ActionResult TableSix(string date = "2017-11-12")
        {
            var view = dbLibrary.GetTableSixViewModel(Convert.ToDateTime(date));

            return View(view);
        }

    }
}