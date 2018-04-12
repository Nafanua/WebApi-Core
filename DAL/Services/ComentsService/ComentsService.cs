using System.Linq;
using DAL.Model;
using DAL.UnitWork;

namespace DAL.Services.ComentsService
{
    public class ComentsService : IComentsService
    {
        private readonly IUnitOfWork unitOfWork;

        public ComentsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddComment(CommentDbo coment)
        {
            unitOfWork.CommentRepository.Add(coment);
            unitOfWork.Save();
        }

        public IQueryable<CommentDbo> GetAll()
        {
            return unitOfWork.CommentRepository.GetAll();
        }

        public void Save()
        {
            unitOfWork.Transaction();
        }
    }
}
