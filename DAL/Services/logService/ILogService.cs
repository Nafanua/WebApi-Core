using DAL.Model;
using System;
using System.Linq;

namespace DAL.Service
{
    public interface ILogService : IDisposable
    {
        IQueryable<LogDbo> GetAll();
        void AddLog(LogDbo log);
    }
}
