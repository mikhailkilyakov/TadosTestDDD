namespace Domain.UnitOfWork
{
    using System;
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}