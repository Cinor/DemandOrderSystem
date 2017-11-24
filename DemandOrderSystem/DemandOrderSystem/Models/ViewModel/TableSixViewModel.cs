using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemandOrderSystem.Models.ViewModel
{
    public class TableSixViewModel
    {
        /// <summary>
        /// 資訊部室
        /// </summary>
        public List<Class6Department> ITDept { get; set; }

        /// <summary>
        /// 時間
        /// </summary>
        public DateTime? Data { get; set; }

        /// <summary>
        /// 總驗收
        /// </summary>
        public int Sum_Acceptance { get { return ITDept.Sum(x => x.維護類別.Sum(y => y.Acceptance)); } }

        /// <summary>
        /// 總受理
        /// </summary>
        public int Sum_Accepted { get { return ITDept.Sum(x => x.維護類別.Sum(y => y.Accepted)); } }
    }

    public class Class6Department
    {
        /// <summary>
        /// 資訊部室
        /// </summary>
        [DisplayName("資訊部室")]
        public string ITDept { get; set; }

        /// <summary>
        /// 維護類別
        /// </summary>
        [DisplayName("維護類別")]
        public List<Class6Detail> 維護類別 { get; set; }
    }

    public class Class6Detail
    {
        /// <summary>
        /// 維護案或業務線別
        /// </summary>
        [DisplayName("維護案或業務線別")]
        public string MaintainLine { get; set; }

        /// <summary>
        /// 受理
        /// </summary>
        public int Accepted { get; set; }

        /// <summary>
        /// 驗收
        /// </summary>
        public int Acceptance { get; set; }

        /// <summary>
        /// 合計
        /// </summary>
        public int Total { get { return Accepted + Acceptance; } }

    }
}