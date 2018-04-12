using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Model;
using DAL.UnitWork;

namespace DAL.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddUser(UserDbo user)
        {
            unitOfWork.UserDboRepository.Add(user);
            unitOfWork.Save();
        }

        public IQueryable<UserDbo> GetAll()
        {
            return unitOfWork.UserDboRepository.GetAll();
        }

        public UserDbo GetById(int id)
        {
            return unitOfWork.UserDboRepository.GetAll().FirstOrDefault(i => i.Id == id);
        }

        public void Save()
        {
            unitOfWork.Save();
        }
    }
}
