using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DemandOrderSystem.Models
{
    public static class DataTableExtensions
    {
        public static List<TResult> ToList<TResult>(this DataTable dtValue) where TResult : class, new()
        {
            //建立一個回傳用的 List<TResult>
            List<TResult> resultList = new List<TResult>();

            //取得映射型別
            Type type = typeof(TResult);

            //儲存 DataTable 的欄位名稱
            List<PropertyInfo> prList = new List<PropertyInfo>();

            foreach (PropertyInfo item in type.GetProperties())
            {
                if (dtValue.Columns.IndexOf(item.Name) != -1)
                    prList.Add(item);
            }

            //逐筆將 DataTable 的值新增到 List<TResult> 中
            foreach (DataRow item in dtValue.Rows)
            {
                TResult tr = new TResult();

                foreach (PropertyInfo pr in prList)
                {
                    if (item[pr.Name] != DBNull.Value)
                        pr.SetValue(tr, item[pr.Name], null);
                }

                resultList.Add(tr);
            }

            return resultList;
        }
    }
}