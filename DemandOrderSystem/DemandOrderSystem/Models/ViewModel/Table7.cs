using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemandOrderSystem.Models.ViewModel
{
    public class Table7
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
        public DateTime? 撤件日期 { get; set; }//撤件日期

    }
}