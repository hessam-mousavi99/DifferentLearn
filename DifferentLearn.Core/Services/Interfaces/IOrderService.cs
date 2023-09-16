using DifferentLearn.Core.DTOs.Order;
using DifferentLearn.Data.Entites.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrderAsync(string username, int courseid);
        Task UpdatePriceOrderAsync(int orderid);
        Task<Order> GetOrderForUserPanelAsync(string username, int orderid);
        Task<Order> GetOrderByIdAsync(int orderid);
        Task<bool> FinalyOrderAsync(string username, int orderid);
        Task<int> GetUserOrderAsync(string username);
        Task<List<Order>> GetUserOrdersAsync(string username);
        Task UpdateOrderAsync(Order order);
        Task<DisCountUseType> UseDisCountAsync(int orderid, string code);

    }
}
