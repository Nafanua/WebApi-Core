using DAL.Model;
using System;
using System.Linq;

namespace DAL.Service
{
    public interface IDatasourceService : IDisposable
    {
        IQueryable<DatasourceDbo> GetAll();
        void AddDatasource(DatasourceDbo datasource);
    }
}
