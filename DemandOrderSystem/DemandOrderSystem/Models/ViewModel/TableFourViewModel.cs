using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemandOrderSystem.Models.ViewModel
{
    public class TableFourViewModel
    {
        public List<CountByMonth> countByMonth { get; set; }
        [DisplayName("評估收件日開始月份")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime startTime { get; set; }
        [DisplayName("評估收件日結束月份")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime endTime { get; set; }
    }

    public class CountByMonth
    {
        [DisplayName("分類名稱")]
        public String classificationName { get; set; }
        public List<int> counts { get; set; }

        public int sum { get; set; }
    }
}