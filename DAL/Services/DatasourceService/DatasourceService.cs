using System.Linq;
using DAL.Model;
using DAL.UnitWork;

namespace DAL.Service
{
    public class DatasourceService : IDatasourceService
    {
        private readonly IUnitOfWork unitOfWork;

        public DatasourceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddDatasource(DatasourceDbo datasource)
        {
            unitOfWork.DatasourceRepository.Add(datasource);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IQueryable<DatasourceDbo> GetAll()
        {
            return unitOfWork.DatasourceRepository.GetAll();
        }
    }
}
