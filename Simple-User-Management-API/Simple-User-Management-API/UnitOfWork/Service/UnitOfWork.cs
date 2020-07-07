

using Microsoft.EntityFrameworkCore;
using Simple_User_Management_API.Models;
using Simple_User_Management_API.UnitOfWork.Interface;

namespace Simple_User_Management_API.UnitOfWork.Service
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly UserManagementContext Context;

        public UnitOfWork(UserManagementContext context)
        {
            Context = context;
        }
        public void Commit()
        {
            Context.SaveChanges();
        }
        public async void CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
           Context.Dispose();
            
        }
    }
}
