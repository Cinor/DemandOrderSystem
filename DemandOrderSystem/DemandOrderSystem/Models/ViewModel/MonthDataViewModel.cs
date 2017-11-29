using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemandOrderSystem.Models.ViewModel
{
    public class MonthDataViewModel
    {
        public string StrSearch { get; set; } = "";

        public DateTime DataMonth { get; set; }

        public string State { get; set; }

        public string DeptName { get; set; }

        public string ApplyDeptName { get; set; }

        public int DeptCode { get; set; }

        public int Page { get; set; } = 1;

        /// <summary>
        /// 回傳部室 若資訊部室有值就用資訊部室
        /// </summary>
        public string Dept
        {
            get
            {
                if (DeptName == null)
                {
                    return ApplyDeptName;
                }
                else
                {
                    return DeptName;
                }
            }
        }

        //public List<Order> Orders { get; set; }

        public IPagedList<Order> Orders { get; set; }
    }
}