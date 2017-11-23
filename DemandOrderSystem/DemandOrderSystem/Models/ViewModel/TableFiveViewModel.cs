using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemandOrderSystem.Models.ViewModel
{
    public class TableFiveViewModel
    {
        public List<SelectListItem> condition { get; set; }

        public List<Class5Table> table { get; set; }
    }

    public class Class5Table
    {
        [DisplayName("資訊部室")]
        public String information_department { get; set; }

        public List<Class5TableByDept> tableByDept { get; set; }

        public int sum { get => tableByDept.Sum(x => x.amount); }
    }

    public class Class5TableByDept
    {
        [DisplayName("維護案或業務線別")]
        public String work_line { get; set; }
        [DisplayName("需求單負責人")]
        public String demand_principal { get; set; }
        [DisplayName("合計")]
        public int amount { get; set; }

    }
}