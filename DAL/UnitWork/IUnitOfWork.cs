using DAL.Model;
using DAL.Repository;
using System;

namespace DAL.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericReposytory<DatasourceDbo> DatasourceRepository { get; }
        IGenericReposytory<LogDbo> LogRepository { get; }
        IGenericReposytory<ItemDbo> ItemRepository { get; }
        IGenericReposytory<UserDbo> UserDboRepository { get; }
        IGenericReposytory<CommentDbo> CommentRepository { get; }

        void Save();
        void Transaction();
    }
}
