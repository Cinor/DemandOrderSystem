using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemandOrderSystem.Service;
using DemandOrderSystem.Models;
using DemandOrderSystem.Models.ViewModel;
using System.Data;

namespace DemandOrderSystem.Library
{
    public class OrderLibrary
    {
        DBService dBService = new DBService();

        public List<Order> getOrderDatas()
        {
            try
            {
                var orderDatas = dBService.getSummaryTable().ToList();

                return orderDatas;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public Order getOrderById(string orderId)
        {
            try
            {
                return dBService.getOrderById(orderId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        /// <summary>
        /// 取得Table3項目
        /// </summary>
        /// <param name="orderState">需求單狀態</param>
        /// <param name="acceptionTestStartDate_0">驗收開始日_範圍起</param>
        /// <param name="acceptionTestStartDate_1">驗收開始日_範圍結</param>
        /// <param name="applyDept">申請人部室</param>
        /// <returns></returns>
        public List<Table3> getOrdersTable3(string orderState, string acceptionTestStartDate_0, string acceptionTestStartDate_1, string applyDept)
        {

            var Table = (from odt in getOrderDatas()
                         where (!string.IsNullOrWhiteSpace(orderState) ? odt.State == orderState : true)
                         & (!string.IsNullOrWhiteSpace(acceptionTestStartDate_0) ? odt.AcceptionTestStartDate >= Convert.ToDateTime(acceptionTestStartDate_0) : true)
                         && (!string.IsNullOrWhiteSpace(acceptionTestStartDate_1) ? odt.AcceptionTestStartDate <= Convert.ToDateTime(acceptionTestStartDate_1) : true)
                         & (!string.IsNullOrWhiteSpace(applyDept) ? odt.ApplyDept == applyDept : true)
                         select new Table3
                         {
                             OrderID = odt.OrderID,
                             OrderName = odt.OrderName,
                             Applicant = odt.Applicant,
                             ApplyDept = odt.ApplyDept,
                             DemandDutyPerson = odt.DemandDutyPerson,
                             AcceptionTestStartDate = odt.AcceptionTestStartDate
                         }).ToList();

            return Table;
        }


        public List<Table7> getOrdersTable7(string 結案日, string 撤件日期, string 申請部室)
        {
            List<Table7> _Table = new List<Table7>();
            DataTable orderDt = new DataTable();

            orderDt = dBService.GetTable7_Details(結案日, 撤件日期);
            _Table = orderDt.ToList<Table7>();

            if (!string.IsNullOrWhiteSpace(申請部室))
            {
                _Table = _Table.Where(o => o.Applicant == 申請部室).ToList();
            }

            return _Table;
        }

        /// <summary>
        /// 取得Table8項目
        /// </summary>
        /// <param name="orderState">需求單狀態</param>
        /// <param name="acceptionTestFinishDate_0">驗收結束日_範圍起</param>
        /// <param name="acceptionTestFinishDate_1">驗收結束日_範圍結</param>
        /// <param name="maintainITDept">維護資訊室</param>
        /// <returns></returns>
        public List<Table8> getOrdersTable8(string orderState, string acceptionTestFinishDate_0, string acceptionTestFinishDate_1,  string maintainITDept)
        {

            var Table = (from odt in getOrderDatas()
                         where (!string.IsNullOrWhiteSpace(orderState) ? odt.State == orderState : true)
                         & (!string.IsNullOrWhiteSpace(acceptionTestFinishDate_0) ? odt.AcceptionTestFinishDate >= Convert.ToDateTime(acceptionTestFinishDate_0) : true)
                         && (!string.IsNullOrWhiteSpace(acceptionTestFinishDate_1) ? odt.AcceptionTestFinishDate <= Convert.ToDateTime(acceptionTestFinishDate_1) : true)
                         & (!string.IsNullOrWhiteSpace(maintainITDept) ? odt.MaintainITDept == maintainITDept : true)
                         select new Table8
                         {
                             MaintainITDept = odt.MaintainITDept,
                             MaintainITSec = odt.MaintainITSec,
                             DemandDutyPerson = odt.DemandDutyPerson,
                             OrderID = odt.OrderID,
                             OrderName = odt.OrderName,
                             State = odt.State,
                             AcceptionTestFinishDate = odt.AcceptionTestFinishDate
                         }).ToList();


            return Table;
        }





    }
}