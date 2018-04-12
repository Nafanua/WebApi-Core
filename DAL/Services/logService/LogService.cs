using DAL.Model;
using DAL.UnitWork;
using System.Linq;

namespace DAL.Service
{
    public class LogService : ILogService
    {
        private readonly IUnitOfWork unitOfWork;

        public LogService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddLog(LogDbo log)
        {
            unitOfWork.LogRepository.Add(log);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IQueryable<LogDbo> GetAll()
        {
            return unitOfWork.LogRepository.GetAll();
        }
    }
}
