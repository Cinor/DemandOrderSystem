using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemandOrderSystem.Service;
using DemandOrderSystem.Models;
using DemandOrderSystem.Models.ViewModel;

namespace DemandOrderSystem.Library
{
    public class OrderLibrary
    {
        DBService dBService = new DBService();

        public List<Order> getOrderDatas()
        {
            try
            {
                var orderDatas = dBService.getSummaryTable().ToList();

                return orderDatas;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public Order getOrderById(string orderId)
        {
            try
            {
                return dBService.getOrderById(orderId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
    }
}