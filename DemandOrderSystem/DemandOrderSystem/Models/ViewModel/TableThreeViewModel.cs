using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemandOrderSystem.Models.ViewModel
{
    public class TableThreeViewModel
    {
        /// <summary>
        /// 需求單號
        /// </summary>
        [DisplayName("需求單號")]
        public string OrderID { get; set; }

        /// <summary>
        /// 申請人部室
        /// </summary>
        [DisplayName("申請人部室")]
        public string ApplyDept { get; set; }

        /// <summary>
        /// 申請人
        /// </summary>
        [DisplayName("申請人")]
        public string Applicant { get; set; }

        /// <summary>
        /// 需求單負責人
        /// </summary>
        [DisplayName("需求單負責人")]
        public string DemandDutyPerson { get; set; }

        /// <summary>
        /// 驗收開始日
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("驗收開始日")]
        public DateTime? AcceptionTestStartDate { get; set; }

        /// <summary>
        /// 需求單主旨
        /// </summary>
        [DisplayName("需求單主旨")]
        public string OrderName { get; set; }

    }
}