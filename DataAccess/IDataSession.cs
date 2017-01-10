namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ReservaSalas.Modelos;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Infrastructure;

    public interface IDataSession : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}