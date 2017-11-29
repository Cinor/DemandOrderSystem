///需求單表一ViewModel
///撰寫人:林紹瑾

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemandOrderSystem.Models.ViewModel
{
    public class TableOneViewModel
    {
        //private readonly List<DepartmentName> _deptName = new List<DepartmentName>{
        //new DepartmentName { Id = 0, Name = "資訊中心" },
        //new DepartmentName { Id =1, Name ="數位資訊部"},
        //new DepartmentName{Id=2, Name="資訊系統部" },
        //new DepartmentName{Id=3, Name="投資資訊部" },
        //new DepartmentName{ Id=4, Name="壽險資訊部"},
        //new DepartmentName{ Id=5, Name="資訊規劃部"} };

        private static Dictionary<int, string> _itDeptName;

        //public List<SimpleOrderViewModel> Orders;
        private List<Order> orders;

        public List<Order> Orders2
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;

                if (_itDeptName == null)
                {
                    _itDeptName = new Dictionary<int, string>();

                    _itDeptName.Add(0, "資訊中心");

                    var i = 1;
                    foreach (var dept in orders.Select(o => o.ITDept).Distinct().OrderBy(o => o))
                    {
                        _itDeptName.Add(i++, dept);
                    }
                }
            }
        }
        //public List<StateCountViewModel> StateList;

        public int selectedDept { get; set; }

        public string selectedDeptName
        {
            get
            {
                return _itDeptName.FirstOrDefault(d => d.Key == selectedDept).Value;
            }
        }

        [DisplayName("搜尋開始日期")]
        [DataType(DataType.Date)]
        [System.Web.Mvc.Remote("DateValid", "DateValid", ErrorMessage = "日期必須在這個月以前")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM}", ApplyFormatInEditMode = true)]
        public DateTime SearchDate { get; set; }

        [DisplayName("資訊部室")]
        public IEnumerable<SelectListItem> DeptList
        {
            get { return new SelectList(_itDeptName, "Key", "Value"); }
        }
    }

    public class DepartmentName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}