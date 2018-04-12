using System;
using DAL.Model;
using DAL.Repository;

namespace DAL.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContext context;

        private IGenericReposytory<DatasourceDbo> datasourceRepository;

        private IGenericReposytory<LogDbo> logRepository;

        private IGenericReposytory<ItemDbo> itemRepository;

        private IGenericReposytory<UserDbo> userDboRepository;

        private IGenericReposytory<CommentDbo> commentRepository;

        private bool disposed = false;

        public UnitOfWork(ModelContext context)
        {
            this.context = context;
        }

        public IGenericReposytory<DatasourceDbo> DatasourceRepository
        {
            get
            {
                if (datasourceRepository == null)
                {
                    datasourceRepository = new GenericReposotory<DatasourceDbo>(context);
                }

                return datasourceRepository;
            }
        }

        public IGenericReposytory<LogDbo> LogRepository
        {
            get
            {
                if (logRepository == null)
                {
                    logRepository = new GenericReposotory<LogDbo>(context);
                }

                return logRepository;
            }
        }

        public IGenericReposytory<ItemDbo> ItemRepository
        {
            get
            {
                if (itemRepository == null)
                {
                    itemRepository = new GenericReposotory<ItemDbo>(context);
                }

                return itemRepository;
            }
        }

        public IGenericReposytory<UserDbo> UserDboRepository
        {
            get
            {
                if (userDboRepository == null)
                {
                    userDboRepository = new GenericReposotory<UserDbo>(context);
                }

                return userDboRepository;
            }
        }

        public IGenericReposytory<CommentDbo> CommentRepository
        {
            get
            {
                if (commentRepository == null)
                {
                    commentRepository = new GenericReposotory<CommentDbo>(context);
                }

                return commentRepository;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();            
        }

        public void Transaction()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
