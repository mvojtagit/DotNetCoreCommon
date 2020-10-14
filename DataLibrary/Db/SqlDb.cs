using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Db
{
    public class SqlDb : IDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDb(IConfiguration config)
        {
            this._config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string strProcedureName, U parameters, string strConnStringName)
        {
            string strCnString = _config.GetConnectionString(strConnStringName);

            using (IDbConnection cn = new SqlConnection(strCnString))
            {
                var rows = await cn.QueryAsync<T>(strProcedureName,
                                                  parameters,
                                                  commandType: CommandType.StoredProcedure);

                return rows.ToList();
            }
        }

        public async Task<int> Save<T>(string strProcedureName, T parameters, string strConnStringName)
        {
            string strCnString = _config.GetConnectionString(strConnStringName);

            using (IDbConnection cn = new SqlConnection(strCnString))
            {
                var result = await cn.ExecuteAsync(strProcedureName,
                                                  parameters,
                                                  commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
