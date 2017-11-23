using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemandOrderSystem.Models.ViewModel
{
    public class TableEightViewModel
    {
        /// <summary>
        /// 需求單號
        /// </summary>
        [DisplayName("需求單號")]
        public string OrderID { get; set; }

        /// <summary>
        /// 維護資訊室
        /// </summary>
        [DisplayName("資訊室")]
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
        /// 需求單主旨
        /// </summary>
        [DisplayName("需求單主旨")]
        public string OrderName { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        [DisplayName("狀態")]
        public string State { get; set; }

        /// <summary>
        /// 驗收結束日
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("驗收結束日")]
        public DateTime? AcceptionTestFinishDate { get; set; }

    }
}