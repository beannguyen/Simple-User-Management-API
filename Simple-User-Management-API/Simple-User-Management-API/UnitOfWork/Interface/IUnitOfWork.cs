using Microsoft.EntityFrameworkCore;
using Simple_User_Management_API.Models;
using System;
using System.Threading.Tasks;

namespace Simple_User_Management_API.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void CommitAsync();
    }
}