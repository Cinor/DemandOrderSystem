using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemandOrderSystem.Models.ViewModel
{
    public class TableTwoViewModel
    {
        public Dictionary<int, string> _applyDeptName = new Dictionary<int, string>();

        [DisplayName("選擇申請部門")]
        public IEnumerable<SelectListItem> DeptList { get; set; }

        //public List<SimpleOrderViewModel> Orders;

        private List<Order> orders;

        public List<Order> Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;

                var group = Orders.GroupBy(m => m.ApplyDept).OrderByDescending(g => g.Where(o => o.State != "結案").Count()).Select(g => g.Key).ToList();

                var i = 1;
                _applyDeptName.Add(0, "所有部門");
                foreach (var g in group)
                {
                    _applyDeptName.Add(i, g);
                    i++;
                }



                DeptList = new SelectList((IEnumerable)_applyDeptName, "Key", "Value");
            }
        }

        [DisplayName("申請部門")]
        public int SelectedDept { get; set; }

        [DisplayName("搜尋開始日期")]
        [DataType(DataType.Date)]
        [System.Web.Mvc.Remote("DateValid", "DateValid", ErrorMessage = "日期必須在這個月以前")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM}", ApplyFormatInEditMode = true)]
        public DateTime SearchDate { get; set; }

        public int Page { get; set; } = 1;
    }
}