using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemandOrderSystem.Service;
using DemandOrderSystem.Models;
using DemandOrderSystem.Models.ViewModel;
using System.Web.Mvc;
using PagedList;
using System.IO;
using NPOI.XSSF.UserModel;
using System.Data;

namespace DemandOrderSystem.Library
{
    public class OrderLibrary
    {
        DBService dBService = new DBService();

        public string[] deptName = new string[] { "資訊中心", "數位資訊部", "資訊系統部", "投資資訊部", "壽險資訊部", "資訊規劃部" };

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
        /// 取得需求單總表，條件參數決定WHERE判斷式，為空則取得全部
        /// </summary>
        /// <param name="applicant_department_list">申請人部室</param>
        /// <param name="information_management_list">維護資訊室</param>
        /// <param name="evaluate_recived_date_start">預計收件日範圍開始</param>
        /// <param name="evaluate_recived_date_end">預計收件日範圍結束</param>
        /// <param name="closed_date_start">結案日範圍開始</param>
        /// <param name="closed_date_end">結案日範圍結束</param>
        /// <param name="currentPage">頁面頁數</param>
        /// <returns></returns>
        public TableAllViewModel GetTableAllViewModel(String applicant_department_list, String information_management_list, DateTime? evaluate_recived_date_start, DateTime? evaluate_recived_date_end, DateTime? closed_date_start, DateTime? closed_date_end, int currentPage)
        {
            TableAllViewModel tableAllViewModel = new TableAllViewModel();
            var orderDatasList = getOrderDatas();
            var list = orderDatasList.OrderBy(c => c.OrderID)
                .Where(c => string.IsNullOrEmpty(applicant_department_list) ? true : c.ApplyDept == applicant_department_list)
                .Where(c => string.IsNullOrEmpty(information_management_list) ? true : c.MaintainITDept == information_management_list)
                .Where(c => evaluate_recived_date_start.HasValue && evaluate_recived_date_end.HasValue ? c.ExpectRecieveDate >= evaluate_recived_date_start && c.ExpectRecieveDate <= evaluate_recived_date_end : true)
                .Where(c => closed_date_start.HasValue && closed_date_end.HasValue ? c.CaseCloseDate >= closed_date_start && c.CaseCloseDate <= closed_date_end : true);
            var result = list.ToPagedList(currentPage, 20);
            tableAllViewModel.orderList = result;
            tableAllViewModel.applicant_department_list = orderDatasList.Where(x => !String.IsNullOrEmpty(x.ApplyDept)).GroupBy(x => x.ApplyDept).Select(x => new SelectListItem { Text = x.Key, Value = x.Key }).ToList();
            tableAllViewModel.information_management_list = orderDatasList.Where(x => !String.IsNullOrEmpty(x.MaintainITDept)).GroupBy(x => x.MaintainITDept).Select(x => new SelectListItem { Text = x.Key, Value = x.Key }).ToList();


            return tableAllViewModel;
        }

        /// <summary>
        /// 取得需求單總表Excel下載檔案
        /// </summary>
        /// <returns></returns>
        public MemoryStream GetTableAllExcelData()
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("總表明細");
            MemoryStream files = new MemoryStream();
            var data = getOrderDatas().ToList();
            sheet.CreateRow(0);
            sheet.GetRow(0).CreateCell(0).SetCellValue("申請人部室");
            sheet.GetRow(0).CreateCell(1).SetCellValue("申請人課別");
            sheet.GetRow(0).CreateCell(2).SetCellValue("申請人");
            sheet.GetRow(0).CreateCell(3).SetCellValue("需求單號");
            sheet.GetRow(0).CreateCell(4).SetCellValue("需求單主旨");
            sheet.GetRow(0).CreateCell(5).SetCellValue("資訊部室");
            sheet.GetRow(0).CreateCell(6).SetCellValue("狀態");
            sheet.GetRow(0).CreateCell(7).SetCellValue("期望完成日");
            sheet.GetRow(0).CreateCell(8).SetCellValue("評估收件日");
            sheet.GetRow(0).CreateCell(9).SetCellValue("預計開始日");
            sheet.GetRow(0).CreateCell(10).SetCellValue("預計結束日");
            sheet.GetRow(0).CreateCell(11).SetCellValue("驗收開始日");
            sheet.GetRow(0).CreateCell(12).SetCellValue("驗收結束日");
            sheet.GetRow(0).CreateCell(13).SetCellValue("結案日");
            sheet.GetRow(0).CreateCell(14).SetCellValue("維護案或業務線別");
            sheet.GetRow(0).CreateCell(15).SetCellValue("維護資訊室");
            sheet.GetRow(0).CreateCell(16).SetCellValue("資訊課");
            sheet.GetRow(0).CreateCell(17).SetCellValue("需求單負責人");
            sheet.GetRow(0).CreateCell(18).SetCellValue("分類名稱(編號)");
            sheet.GetRow(0).CreateCell(19).SetCellValue("分類中文(名稱)");

            for (int i = 0; i < data.Count; i++)
            {
                sheet.CreateRow(i + 1);
                sheet.GetRow(i + 1).CreateCell(0).SetCellValue(data[i].ApplyDept);
                sheet.GetRow(i + 1).CreateCell(1).SetCellValue(data[i].ApplySec);
                sheet.GetRow(i + 1).CreateCell(2).SetCellValue(data[i].Applicant);
                sheet.GetRow(i + 1).CreateCell(3).SetCellValue(data[i].OrderID);
                sheet.GetRow(i + 1).CreateCell(4).SetCellValue(data[i].OrderName);
                sheet.GetRow(i + 1).CreateCell(5).SetCellValue(data[i].ITDept);
                sheet.GetRow(i + 1).CreateCell(6).SetCellValue(data[i].State);
                sheet.GetRow(i + 1).CreateCell(7).SetCellValue(data[i].ExpectCompleteDate.HasValue ? data[i].ExpectCompleteDate.Value.ToShortDateString() : "");
                sheet.GetRow(i + 1).CreateCell(8).SetCellValue(data[i].ExpectRecieveDate.HasValue ? data[i].ExpectRecieveDate.Value.ToShortDateString() : "");
                sheet.GetRow(i + 1).CreateCell(9).SetCellValue(data[i].ExpectStartDate.HasValue ? data[i].ExpectStartDate.Value.ToShortDateString() : "");
                sheet.GetRow(i + 1).CreateCell(10).SetCellValue(data[i].ExpectFinishDate.HasValue ? data[i].ExpectFinishDate.Value.ToShortDateString() : "");
                sheet.GetRow(i + 1).CreateCell(11).SetCellValue(data[i].AcceptionTestStartDate.HasValue ? data[i].AcceptionTestStartDate.Value.ToShortDateString() : "");
                sheet.GetRow(i + 1).CreateCell(12).SetCellValue(data[i].AcceptionTestFinishDate.HasValue ? data[i].AcceptionTestFinishDate.Value.ToShortDateString() : "");
                sheet.GetRow(i + 1).CreateCell(13).SetCellValue(data[i].CaseCloseDate.HasValue ? data[i].CaseCloseDate.Value.ToShortDateString() : "");
                sheet.GetRow(i + 1).CreateCell(14).SetCellValue(data[i].MaintainLine);
                sheet.GetRow(i + 1).CreateCell(15).SetCellValue(data[i].MaintainITDept);
                sheet.GetRow(i + 1).CreateCell(16).SetCellValue(data[i].MaintainITSec);
                sheet.GetRow(i + 1).CreateCell(17).SetCellValue(data[i].DemandDutyPerson);
                sheet.GetRow(i + 1).CreateCell(18).SetCellValue(data[i].ClassNo);
                sheet.GetRow(i + 1).CreateCell(19).SetCellValue(data[i].ClassName);
            }

            workbook.Write(files);


            return files;
        }

        /// <summary>
        /// 已交付UAT但尚未結案之明細表
        /// </summary>
        /// <param name="orderState">需求單狀態</param>
        /// <param name="acceptionTestStartDate_0">驗收開始日_範圍起</param>
        /// <param name="acceptionTestStartDate_1">驗收開始日_範圍結</param>
        /// <param name="applyDept">申請人部室</param>
        /// <param name="orderID">需求單號</param>
        /// <param name="currentPage">頁面號碼</param>
        /// <returns></returns>
        public TableThreeViewModel GetTableThreeViewModel(string orderState, string acceptionTestStartDate_0, string acceptionTestStartDate_1, string applyDept, string orderID, int currentPage)
        {

            try
            {
                TableThreeViewModel tableThreeViewModel = new TableThreeViewModel();

                if (string.IsNullOrWhiteSpace(acceptionTestStartDate_1))
                {
                    acceptionTestStartDate_1 = DateTime.Now.Date.ToString();
                }

                var orderDatasList = getOrderDatas();
                var Table = (from odt in orderDatasList
                             where (!string.IsNullOrWhiteSpace(orderState) ? odt.State == orderState : true)
                             & (!string.IsNullOrWhiteSpace(applyDept) ? odt.ApplyDept == applyDept : true)
                             & (!string.IsNullOrWhiteSpace(orderID) ? odt.OrderID == orderID : true)
                             & (!string.IsNullOrWhiteSpace(acceptionTestStartDate_0) ? odt.AcceptionTestStartDate >= Convert.ToDateTime(acceptionTestStartDate_0) : true)
                             & (!string.IsNullOrWhiteSpace(acceptionTestStartDate_1) ? odt.AcceptionTestStartDate <= Convert.ToDateTime(acceptionTestStartDate_1) : true)
                             select new TableThree
                             {
                                 OrderID = odt.OrderID,
                                 OrderName = odt.OrderName,
                                 Applicant = odt.Applicant,
                                 ApplyDept = odt.ApplyDept,
                                 DemandDutyPerson = odt.DemandDutyPerson,
                                 AcceptionTestStartDate = odt.AcceptionTestStartDate
                             }).ToList();

                tableThreeViewModel.TableThree = Table.ToPagedList(currentPage, 20);
                tableThreeViewModel.applicant_department_list = orderDatasList.GroupBy(x => x.ApplyDept).Select(x => new SelectListItem() { Text = x.Key.ToString(), Value = x.Key.ToString() }).Distinct().ToList();
                tableThreeViewModel.Count = Table.Count().ToString();

                return tableThreeViewModel;
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        /// <summary>
        /// 取得需求單分類統計表依傳入啟始結束日查詢
        /// </summary>
        /// <param name="startTime">查詢範圍日開始</param>
        /// <param name="endTime">查詢範圍日結束</param>
        /// <returns></returns>
        public TableFourViewModel GetTableFourViewModel(DateTime startTime, DateTime endTime)
        {
            TableFourViewModel model = new TableFourViewModel();
            List<CountByMonth> q1 = (from rs in getOrderDatas()
                                     where rs.ClassNo != null && rs.ClassNo != "00" && rs.ClassNo != "32"
                                     group rs by rs.ClassNo into t
                                     from rs2 in t
                                     orderby rs2.ClassNo
                                     select new CountByMonth()
                                     {
                                         classificationName = rs2.ClassName
                                     }).GroupBy(x => x.classificationName).Select(g => g.First()).ToList();

            List<DateTime> range = new List<DateTime>();
            DateTime part = new DateTime(startTime.Year, startTime.Month, startTime.Day);
            while (part <= endTime)
            {
                range.Add(new DateTime(part.Year, part.Month, part.Day));
                part = part.AddMonths(1);
            }
            var queryable = getOrderDatas().Where(c => c.ExpectRecieveDate != null).
                Select(x => new
                {
                    eva_year_month = x.ExpectRecieveDate.Value.Year + "-" + x.ExpectRecieveDate.Value.Month,
                    OrderID = x.OrderID,
                    ApplyDept = x.ApplyDept,
                    ApplySec = x.ApplySec,
                    Applicant = x.Applicant,
                    OrderName = x.OrderName,
                    ITDept = x.ITDept,
                    State = x.State,
                    ExpectCompleteDate = x.ExpectCompleteDate,
                    ExpectRecieveDate = x.ExpectRecieveDate,
                    ExpectStartDate = x.ExpectStartDate,
                    ExpectFinishDate = x.ExpectFinishDate,
                    AcceptionTestStartDate = x.AcceptionTestStartDate,
                    AcceptionTestFinishDate = x.AcceptionTestFinishDate,
                    CaseCloseDate = x.CaseCloseDate,
                    MaintainLine = x.MaintainLine,
                    MaintainITDept = x.MaintainITDept,
                    MaintainITSec = x.MaintainITSec,
                    DemandDutyPerson = x.DemandDutyPerson,
                    ClassNo = x.ClassNo,
                    ClassName = x.ClassName
                });
            var q2 = (from rs in queryable
                      where rs.ClassNo != null && rs.ClassNo != "00" && rs.ClassNo != "32" && rs.ExpectRecieveDate != null
                      group rs by new { rs.ClassNo, rs.eva_year_month } into t
                      from rs2 in t
                      select new
                      {
                          t.Key,
                          category = rs2.ClassName,
                          count = t.Count(),
                          yearMonth = rs2.eva_year_month
                      }).Distinct().OrderBy(x => x.category).ToList();

            for (int i = 0; i < q1.Count; i++)
            {
                q1[i].counts = new List<int>();
                for (int j = 0; j < range.Count; j++)
                {
                    for (int k = 0; k < q2.Count; k++)
                    {
                        if (q1[i].classificationName == q2[k].category)
                        {
                            if (range[j].ToString("yyyy-M") == q2[k].yearMonth)
                            {
                                q1[i].counts.Add(q2[k].count);
                                q1[i].sum += q2[k].count;
                            }
                        }
                    }
                    if (q1[i].counts.Count <= j)
                    {
                        q1[i].counts.Add(0);
                    }
                }
            }

            model.countByMonth = q1;
            return model;
        }

        /// <summary>
        /// 取得需求單未結案且未回覆預估完成日件數統計及明細表
        /// </summary>
        /// <param name="condition">查詢需求單狀態</param>
        /// <returns></returns>
        public TableFiveViewModel GetTableFiveViewModel(String condition)
        {
            TableFiveViewModel result = new TableFiveViewModel();
            List<Class5Table> class5TableList = new List<Class5Table>();
            var tt = getOrderDatas();
            var class5Queryable = getOrderDatas().Where(x => (x.State == "受理" || x.State == "驗收") && x.ExpectFinishDate == null).Where(x => string.IsNullOrEmpty(condition) ? true : x.State == condition);
            String[] dept = { "壽險資訊部", "數位資訊部", "資訊規劃部", "投資資訊部" };

            //分部室存入報表
            foreach (var item in dept)
            {

                Class5Table table = new Class5Table();
                table.information_department = item;

                var tableByDept = from t in class5Queryable.Where(x => x.MaintainITDept == item)
                                  group t by new { t.MaintainLine, t.DemandDutyPerson } into groupT
                                  select new Class5TableByDept()
                                  {
                                      work_line = groupT.Key.MaintainLine,
                                      demand_principal = groupT.Key.DemandDutyPerson,
                                      amount = groupT.Count()
                                  };

                table.tableByDept = tableByDept.ToList();


                class5TableList.Add(table);
            }
            result.table = class5TableList;

            result.condition = new List<SelectListItem>();
            result.condition.Add(new SelectListItem { Text = "受理", Value = "受理" });
            result.condition.Add(new SelectListItem { Text = "驗收", Value = "驗收" });

            return result;
        }

        /// <summary>
        /// 各業務線別逾預估完成日未結案件數統計及明細表
        /// </summary>
        /// <param name="expectFinishDate">預估完成日小於</param>
        /// <returns></returns>
        public TableSixViewModel GetTableSixViewModel(DateTime expectFinishDate)
        {


            TableSixViewModel viewclass = new TableSixViewModel();
            viewclass.ITDept = new List<Class6Department>();

            var result = getOrderDatas().Where(x => x.ExpectFinishDate < expectFinishDate);

            string[] dept = { "壽險資訊部", "資訊規劃部", "業務資訊部", "投資資訊部" };


            foreach (var item in dept)
            {
                Class6Department Department = new Class6Department();
                Department.ITDept = item;


                var allworklinebydepartment = from i in result.Where(x => x.ITDept == item)
                                              group i by i.MaintainLine into groupT
                                              select new
                                              {
                                                  work_line = groupT.Key == null ? "(空白)" : groupT.Key
                                              };

                var worklinebydepartment = from i in result.Where(x => x.ITDept == item & (x.State == "驗收" || x.State == "受理"))
                                           group i by i.MaintainLine into groupT2
                                           select new
                                           {
                                               work_line2 = groupT2.Key == null ? "(空白)" : groupT2.Key,
                                               Accepted = groupT2.Count(x => x.State == "受理"),
                                               Acceptance = groupT2.Count(x => x.State == "驗收")
                                           };

                var worklinejoin = from i in allworklinebydepartment
                                   join j in worklinebydepartment on i.work_line equals j.work_line2 into g
                                   from j in g.DefaultIfEmpty()
                                   select new Class6Detail()
                                   {
                                       MaintainLine = i.work_line,
                                       Accepted = j != null ? j.Accepted : 0,
                                       Acceptance = j != null ? j.Acceptance : 0
                                   };

                Department.維護類別 = worklinejoin.ToList();

                viewclass.ITDept.Add(Department);

            }


            return viewclass;
        }


        /// <summary>
        /// 撤件明細
        /// </summary>
        /// <param name="orderID">需求單號</param>
        /// <param name="caseCloseDate">結案日</param>
        /// <param name="dischargeDate">撤件日期</param>
        /// <param name="applyDept">申請部室</param>
        /// <param name="currentPage">頁碼</param>
        /// <returns></returns>
        public TableSevenViewModel GetTableSevenViewModel(string orderID, string applyDept, string caseCloseDate, string dischargeDate, int currentPage)
        {
            TableSevenViewModel tableSevenViewModel = new TableSevenViewModel();

            var orderDatasList = dBService.getSevenTable(Convert.ToDateTime(!string.IsNullOrWhiteSpace(caseCloseDate) ? caseCloseDate : caseCloseDate = "2001-01-01")
                          , Convert.ToDateTime(!string.IsNullOrWhiteSpace(dischargeDate) ? dischargeDate : dischargeDate = "2001-01-01")).AsEnumerable();
            var resule = (from dt in orderDatasList
                          where (!string.IsNullOrWhiteSpace(orderID) ? dt.OrderID == orderID : true)
                          & (!string.IsNullOrWhiteSpace(applyDept) ? dt.ApplyDept == applyDept : true)
                          select dt).ToList();
            
            tableSevenViewModel.TableSeven = resule.ToPagedList(currentPage, 20);
            tableSevenViewModel.applicant_department_list = orderDatasList.GroupBy(x => x.ApplyDept).Select(x => new SelectListItem() { Text = x.Key.ToString(), Value = x.Key.ToString() }).Distinct().ToList();
            tableSevenViewModel.Count = resule.Count().ToString();


            return tableSevenViewModel;

        }

        /// <summary>
        /// 已完成驗收但尚未結案
        /// </summary>
        /// <param name="orderID">需求單號</param>
        /// <param name="orderState">需求單狀態</param>
        /// <param name="maintainITDept">維護資訊室</param>
        /// <param name="acceptionTestFinishDate_0">驗收結束日_範圍起</param>
        /// <param name="acceptionTestFinishDate_1">驗收結束日_範圍結</param>
        /// <param name="currentPage">頁碼</param>
        /// <returns></returns>
        public TableEightViewModel GetTableEightViewModel(string orderID, string orderState, string maintainITDept,  string acceptionTestFinishDate_0, string acceptionTestFinishDate_1, int currentPage)
        {

            try
            {
                TableEightViewModel tableEightViewModel = new TableEightViewModel();

                if (string.IsNullOrWhiteSpace(acceptionTestFinishDate_1))
                {
                    acceptionTestFinishDate_1 = DateTime.Now.Date.ToString();
                }
                
                var orderDatasList = getOrderDatas();
                var Table = (from odt in orderDatasList
                             where (!string.IsNullOrWhiteSpace(orderState) ? odt.State == orderState : true)
                             & (!string.IsNullOrWhiteSpace(orderID) ? odt.OrderID == orderID : true)
                             & (!string.IsNullOrWhiteSpace(maintainITDept) ? odt.MaintainITDept == maintainITDept : true)
                             & (!string.IsNullOrWhiteSpace(acceptionTestFinishDate_0) ? odt.AcceptionTestFinishDate >= Convert.ToDateTime(acceptionTestFinishDate_0) : true)
                             & (!string.IsNullOrWhiteSpace(acceptionTestFinishDate_1) ? odt.AcceptionTestFinishDate <= Convert.ToDateTime(acceptionTestFinishDate_1) : true)
                             select new TableEight
                             {
                                 MaintainITDept = odt.MaintainITDept,
                                 MaintainITSec = odt.MaintainITSec,
                                 DemandDutyPerson = odt.DemandDutyPerson,
                                 OrderID = odt.OrderID,
                                 OrderName = odt.OrderName,
                                 State = odt.State,
                                 AcceptionTestFinishDate = odt.AcceptionTestFinishDate
                             }).ToList();

                tableEightViewModel.TableEight = Table.ToPagedList(currentPage, 20);
                tableEightViewModel.maintainITDept_list = orderDatasList.Where(x => x.MaintainITDept != null).GroupBy(x => x.MaintainITDept).Select(x => new SelectListItem() { Text = x.Key.ToString(), Value = x.Key.ToString() }).Distinct().ToList();
                tableEightViewModel.Count = Table.Count().ToString();

                return tableEightViewModel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 取得該資訊部室的需求單並設定時間(起始日期往後一年以前的所有需求單)
        /// </summary>
        /// <param name="deptCode">部室編號string[] { "資訊中心", "數位資訊部", "資訊系統部", "投資資訊部", "壽險資訊部", "資訊規劃部" };</param>
        /// <param name="searchdate">起始日期</param>
        /// <returns></returns>
        public List<Order> GetDataByDept(int deptCode, DateTime searchdate)
        {
            string dept = deptName[deptCode];

            var orderList = getOrderDatas();

            if (dept != "資訊中心")
                return orderList.Where(m => m.ITDept == dept)
                                .Where(m => m.ExpectRecieveDate != null)
                                .Where(m => (m.ExpectRecieveDate.GetValueOrDefault().Date < searchdate.AddYears(1).Date || (m.ExpectRecieveDate.GetValueOrDefault().Year == searchdate.AddYears(1).Year && m.ExpectRecieveDate.GetValueOrDefault().Month == searchdate.AddYears(1).Month)))
                                .ToList();
            else
                return orderList.Where(m => m.ExpectRecieveDate != null)
                                .Where(m => (m.ExpectRecieveDate.GetValueOrDefault().Date < searchdate.AddYears(1).Date || (m.ExpectRecieveDate.GetValueOrDefault().Year == searchdate.AddYears(1).Year && m.ExpectRecieveDate.GetValueOrDefault().Month == searchdate.AddYears(1).Month)))
                                .ToList();

            //return orderList;
        }

        /// <summary>
        /// 取得該月份該狀態以及特定部門的需求單
        /// </summary>
        /// <param name="searchdate">月份datetime</param>
        /// <param name="state">狀態</param>
        /// <param name="deptcode">部室編號{ "資訊中心", "數位資訊部", "資訊系統部", "投資資訊部", "壽險資訊部", "資訊規劃部" }</param>
        /// <returns></returns>
        public List<Order> GetDatasByMonthAndStateAndDept(DateTime searchdate, string state, int deptcode)
        {
            List<Order> dataList = getOrderDatas();

            string dept = deptName[deptcode];

            switch (state)
            {
                case "受理":
                    dataList = dataList.Where(model => (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                            .OrderBy(model => model.ExpectStartDate)
                                            .ToList();
                    break;
                case "結案":
                    dataList = dataList.Where(model => (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                            .Where(model => model.State == "結案")
                                            .OrderBy(model => model.ExpectStartDate)
                                            .ToList();

                    break;
                case "未完成":
                    dataList = dataList.Where(model => (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                            .Where(model => model.State != "結案")
                                            .OrderBy(model => model.ExpectStartDate)
                                            .ToList();

                    break;
                //從以前到現在的資料
                case "累計未完成":
                    dataList = dataList.Where(model => model.ExpectRecieveDate.Value.Date < searchdate.Date || (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                              .Where(model => model.State != "結案")
                                              .OrderBy(model => model.ExpectStartDate)
                                              .ToList();

                    break;
                default:
                    dataList = dataList.Where(model => (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                              .OrderBy(model => model.ExpectStartDate)
                                              .ToList();
                    break;
            }

            //沒有資訊中心這個分類
            if (dept != "資訊中心")
            {
                dataList = dataList.Where(model => model.ITDept == dept).ToList();
            }


            return dataList;
            //return null;
        }

        /// <summary>
        /// 月份+狀態+申請部室 取得需求單
        /// </summary>
        /// <param name="searchdate">月份</param>
        /// <param name="state">狀態</param>
        /// <param name="applyDept">申請部室</param>
        /// <returns></returns>
        public List<Order> GetDatasByMonthAndStateAndApplydept(DateTime searchdate, string state, string applyDept)
        {
            var dataList = getOrderDatas();

            string dept = applyDept;

            switch (state)
            {
                case "受理":
                    dataList = dataList.Where(model => (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                            .OrderBy(model => model.ExpectStartDate)
                                            .ToList();
                    break;
                case "結案":
                    dataList = dataList.Where(model => (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                            .Where(model => model.State == "結案")
                                            .OrderBy(model => model.ExpectStartDate)
                                            .ToList();

                    break;
                case "未完成":
                    dataList = dataList.Where(model => (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                            .Where(model => model.State != "結案")
                                            .OrderBy(model => model.ExpectStartDate)
                                            .ToList();

                    break;
                //從以前到現在的資料
                case "累計未完成":
                    dataList = dataList.Where(model => model.ExpectRecieveDate.Value.Date < searchdate.Date || (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                              .Where(model => model.State != "結案")
                                              .OrderBy(model => model.ExpectStartDate)
                                              .ToList();

                    break;
                default:
                    dataList = dataList.Where(model => (model.ExpectRecieveDate.Value.Year == searchdate.Year && model.ExpectRecieveDate.Value.Month == searchdate.Month))
                                              .OrderBy(model => model.ExpectStartDate)
                                              .ToList();
                    break;
            }

            //沒有資訊中心這個分類
            if (dept != "所有部門")
            {
                dataList = dataList.Where(model => model.ApplyDept == dept).ToList();
            }


            return dataList;
        }

        /// <summary>
        /// 取得輸入日期後一年 之前的所有資料
        /// </summary>
        /// <param name="searchdate">輸入日期</param>
        /// <returns></returns>
        public List<Order> GetDataSearchDate(DateTime searchdate)
        {
            var orderList = getOrderDatas();

            return orderList.Where(m => (m.ExpectRecieveDate.GetValueOrDefault().Date < searchdate.AddYears(1).Date || (m.ExpectRecieveDate.GetValueOrDefault().Year == searchdate.AddYears(1).Year && m.ExpectRecieveDate.GetValueOrDefault().Month == searchdate.AddYears(1).Month)))
                            .ToList();
        }

        /// <summary>
        /// 取得輸入資料中申請部門為輸入的部門
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="applyDept"></param>
        /// <returns></returns>
        public List<Order> ReturnDataByApplydept(List<Order> datas, string applyDept)
        {
            List<Order> result;

            if (applyDept == "所有部門")
            {
                result = datas;
            }
            else
            {
                result = datas.Where(o => o.ApplyDept == applyDept).ToList();
            }

            return result;
        }
    }
}