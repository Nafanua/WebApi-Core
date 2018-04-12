using DAL.Model;
using System.Linq;

namespace DAL.Services.ComentsService
{
    public interface IComentsService
    {
        IQueryable<CommentDbo> GetAll();
        void AddComment(CommentDbo coment);
        void Save();
    }
}
