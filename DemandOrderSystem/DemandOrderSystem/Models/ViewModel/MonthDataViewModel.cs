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

        public List<Order> Orders { get; set; }
    }
}