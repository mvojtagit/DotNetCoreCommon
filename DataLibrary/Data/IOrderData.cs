using DataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public interface IOrderData
    {
        Task<int> CreateOrder(OrderModel om);
        Task<int> DeleteOrder(int orderId);
        Task<List<OrderModel>> GetAll();
        Task<OrderModel> GetOrderByID(int idRow);
        Task<int> UpdateOrder(int orderID, string strOrderName);
    }
}