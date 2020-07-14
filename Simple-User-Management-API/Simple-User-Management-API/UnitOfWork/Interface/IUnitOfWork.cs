using System;

namespace Simple_User_Management_API.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void CommitAsync();
    }
}