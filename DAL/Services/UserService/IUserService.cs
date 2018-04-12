using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public interface IUserService
    {
        IQueryable<UserDbo> GetAll();
        void AddUser(UserDbo datasource);
        UserDbo GetById(int id);
        void Save();
    }
}
