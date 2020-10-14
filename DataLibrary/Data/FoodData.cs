using DataLibrary.Db;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public class FoodData : IFoodData
    {
        private readonly IDataAccess _dac;
        private readonly ConnectionStringData _cnName;

        public FoodData(IDataAccess dac, ConnectionStringData cnName)
        {
            this._dac = dac;
            this._cnName = cnName;
        }

        public Task<List<FoodModel>> GetFood()
        {
            return _dac.LoadData<FoodModel, dynamic>("dbo.spFood_All",
                                                           new { },
                                                           _cnName.SqlConnectionName);
        }

    }
}
