using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Db
{
    public interface IDataAccess
    {
        Task<List<T>> LoadData<T, U>(string strProcedureName, U parameters, string strConnStringName);
        Task<int> Save<T>(string strProcedureName, T parameters, string strConnStringName);
    }
}