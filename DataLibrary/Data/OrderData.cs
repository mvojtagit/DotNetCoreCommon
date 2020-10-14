using Dapper;
using DataLibrary.Db;
using DataLibrary.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLibrary.Data
{
    public class OrderData : IOrderData
    {
        private readonly IDataAccess _IDataAccess;
        private readonly ConnectionStringData _cn;

        public OrderData(IDataAccess idc, ConnectionStringData cn)
        {
            this._IDataAccess = idc;
            this._cn = cn;
        }

        public async Task<int> CreateOrder(OrderModel om)
        {
            /*
             *[dbo].[spOrders_Insert]
	            @OrderName nvarchar(50),
	            @OrderDate datetime2(7),
	            @FoodId int,
	            @Quantity int,
	            @Total money,
	            @Id int output
             */

            DynamicParameters dp = new DynamicParameters();
            dp.Add("OrderName", om.OrderName);
            dp.Add("OrderDate", om.OrderDate);
            dp.Add("FoodId", om.FoodId);
            dp.Add("Quantity", om.Quantity);
            dp.Add("Total", om.Total);
            dp.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _IDataAccess.Save("dbo.spOrders_Insert", dp, _cn.SqlConnectionName);

            return dp.Get<int>("Id");
        }

        public Task<int> UpdateOrder(int orderID, string strOrderName)
        {
            /*

                [dbo].[spOrders_UpdateName]
	            @Id int,
	            @OrderName nvarchar(50)
            */

            DynamicParameters dp = new DynamicParameters();
            dp.Add("Id", orderID);
            dp.Add("OrderName", strOrderName);

            return _IDataAccess.Save("dbo.spOrders_UpdateName", dp, _cn.SqlConnectionName);


        }

        public Task<int> DeleteOrder(int orderId)
        {
            /*

              [dbo].[spOrders_Delete]
	            @Id int
            */
            return _IDataAccess.Save("dbo.spOrders_Delete",
                                     new
                                     {
                                         Id = orderId
                                     },
                                     _cn.SqlConnectionName);


        }

        public async Task<OrderModel> GetOrderByID(int idRow)
        {

            /*
             * [dbo].[spOrders_GetById]
	            @Id int
             */
            var rows = await _IDataAccess.LoadData<OrderModel, dynamic>("dbo.spOrders_GetById",
                                                           new
                                                           {
                                                               Id = idRow
                                                           },
                                                           _cn.SqlConnectionName);
            return rows.FirstOrDefault();

        }

        public Task<List<OrderModel>> GetAll()
        {
            return _IDataAccess.LoadData<OrderModel, dynamic>("dbo.spOrders_All", null, _cn.SqlConnectionName);
        }

    }
}
