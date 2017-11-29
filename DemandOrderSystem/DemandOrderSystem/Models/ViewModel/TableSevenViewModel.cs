using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemandOrderSystem.Models.ViewModel
{
    public class TableSevenViewModel
    {

        /// <summary>
        /// 申請人部室選擇
        /// </summary>
        public List<SelectListItem> applicant_department_list { get; set; }

        /// <summary>
        /// TableThree細項
        /// </summary>
        public IPagedList<TableSeven> TableSeven { get; set; }

        /// <summary>
        /// 總數量
        /// </summary>
        public string Count { get; set; }

    }


    public class TableSeven
    {
        /// <summary>
        /// 需求單號
        /// </summary>
        [DisplayName("需求單號")]
        public string OrderID { get; set; }

        /// <summary>
        /// 申請部室
        /// </summary>
        [DisplayName("申請部室")]
        public string ApplyDept { get; set; }

        /// <summary>
        /// 申請課別
        /// </summary>
        [DisplayName("申請課別")]
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
        /// 撤件日期
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("撤件日期")]
        public DateTime? DischargeDate { get; set; }

    }
}