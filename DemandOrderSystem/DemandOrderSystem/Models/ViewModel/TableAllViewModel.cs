using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DemandOrderSystem.Models.ViewModel
{
    public class TableAllViewModel
    {
        public IPagedList<Order> orderList { get; set; }
        public List<SelectListItem> applicant_department_list { get; set; }
        public List<SelectListItem> information_management_list { get; set; }
        [DisplayName("評估收件日開始")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? evaluate_recived_date_start { get; set; }
        [DisplayName("評估收件日結束")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? evaluate_recived_date_end { get; set; }
        [DisplayName("結案日開始")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? closed_date_start { get; set; }
        [DisplayName("結案日結束")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? closed_date_end { get; set; }
    }
    
}