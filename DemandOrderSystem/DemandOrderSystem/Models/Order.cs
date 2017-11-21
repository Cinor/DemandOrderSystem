///需求單基本類別
///撰寫人:林紹瑾
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemandOrderSystem.Models
{
    public class Order
    {
        /// <summary>
        /// 需求單編號
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 申請人部室
        /// </summary>
        public string ApplyDept { get; set; }

        /// <summary>
        /// 申請人課別
        /// </summary>
        public string ApplySec { get; set; }

        /// <summary>
        /// 申請人
        /// </summary>
        public string Applicant { get; set; }

        /// <summary>
        /// 需求單主旨
        /// </summary>
        public string OrderName { get; set; }

        /// <summary>
        /// 資訊部室
        /// </summary>
        public string ITDept { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 期望完成日
        /// </summary>
        public DateTime? ExpectCompleteDate { get; set; }

        /// <summary>
        /// 評估收件日
        /// </summary>
        public DateTime? ExpectRecieveDate { get; set; }

        /// <summary>
        /// 預計開始日
        /// </summary>
        public DateTime? ExpectStartDate { get; set; }

        /// <summary>
        /// 預計結束日
        /// </summary>
        public DateTime? ExpectFinishDate { get; set; }

        /// <summary>
        /// 驗收開始日
        /// </summary>
        public DateTime? AcceptionTestStartDate { get; set; }

        /// <summary>
        /// 驗收結束日
        /// </summary>
        public DateTime? AcceptionTestFinishDate { get; set; }

        /// <summary>
        /// 結案日
        /// </summary>
        public DateTime? CaseCloseDate { get; set; }


        /// <summary>
        /// 維護案或業務線別
        /// </summary>
        public string MaintainLine { get; set; }

        /// <summary>
        /// 維護資訊室
        /// </summary>
        public string MaintainITDept { get; set; }

        /// <summary>
        /// 資訊課
        /// </summary>
        public string MaintainITSec { get; set; }

        /// <summary>
        /// 需求單負責人
        /// </summary>
        public string DemandDutyPerson { get; set; }

        /// <summary>
        /// 分類名稱(編號)
        /// </summary>
        public string ClassNo { get; set; }

        /// <summary>
        /// 分類中文(名稱)
        /// </summary>
        public string ClassName { get; set; }
    }
}