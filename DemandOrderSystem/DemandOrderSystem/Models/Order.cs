///需求單基本類別
///撰寫人:林紹瑾
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemandOrderSystem.Models
{
    public class Order
    {
        /// <summary>
        /// 需求單編號
        /// </summary>
        [DisplayName("需求單編號")]
        public string OrderID { get; set; }

        /// <summary>
        /// 申請人部室
        /// </summary>
        [DisplayName("申請人部室")]
        public string ApplyDept { get; set; }

        /// <summary>
        /// 申請人課別
        /// </summary>
        [DisplayName("申請人課別")]
        public string ApplySec { get; set; }

        /// <summary>
        /// 申請人
        /// </summary>
        [DisplayName("申請人")]
        public string Applicant { get; set; }

        /// <summary>
        /// 需求單主旨
        /// </summary>
        [DisplayName("需求單主旨")]
        public string OrderName { get; set; }

        /// <summary>
        /// 資訊部室
        /// </summary>
        [DisplayName("資訊部室")]
        public string ITDept { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        [DisplayName("狀態")]
        public string State { get; set; }

        /// <summary>
        /// 期望完成日
        /// </summary>
        [DisplayName("期望完成日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ExpectCompleteDate { get; set; }

        /// <summary>
        /// 評估收件日
        /// </summary>
        [DisplayName("評估收件日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ExpectRecieveDate { get; set; }

        /// <summary>
        /// 預計開始日
        /// </summary>
        [DisplayName("預計開始日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ExpectStartDate { get; set; }

        /// <summary>
        /// 預計結束日
        /// </summary>
        [DisplayName("預計結束日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ExpectFinishDate { get; set; }

        /// <summary>
        /// 驗收開始日
        /// </summary>
        [DisplayName("驗收開始日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? AcceptionTestStartDate { get; set; }

        /// <summary>
        /// 驗收結束日
        /// </summary>
        [DisplayName("驗收結束日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? AcceptionTestFinishDate { get; set; }

        /// <summary>
        /// 結案日
        /// </summary>
        [DisplayName("結案日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CaseCloseDate { get; set; }


        /// <summary>
        /// 維護案或業務線別
        /// </summary>
        [DisplayName("維護案或業務線別")]
        public string MaintainLine { get; set; }

        /// <summary>
        /// 維護資訊室
        /// </summary>
        [DisplayName("維護資訊室")]
        public string MaintainITDept { get; set; }

        /// <summary>
        /// 資訊課
        /// </summary>
        [DisplayName("資訊課")]
        public string MaintainITSec { get; set; }

        /// <summary>
        /// 需求單負責人
        /// </summary>
        [DisplayName("需求單負責人")]
        public string DemandDutyPerson { get; set; }

        /// <summary>
        /// 分類名稱(編號)
        /// </summary>
        [DisplayName("分類名稱(編號)")]
        public string ClassNo { get; set; }

        /// <summary>
        /// 分類中文(名稱)
        /// </summary>
        [DisplayName("分類中文(名稱)")]
        public string ClassName { get; set; }
    }
}