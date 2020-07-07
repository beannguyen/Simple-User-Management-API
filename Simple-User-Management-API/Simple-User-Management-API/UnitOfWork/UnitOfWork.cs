using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Simple_User_Management_API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public System.Data.Entity.DbContext Context { get; }

        Microsoft.EntityFrameworkCore.DbContext IUnitOfWork.Context => throw new NotImplementedException();

        public UnitOfWork(System.Data.Entity.DbContext context)
        {
            Context = context;
        }
        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();

        }
    }
}
