using IziTradeOff.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IziTradeOff.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Aplica el repositorio generico
        /// </summary>
        /// <typeparam name="TEntity">Entidad</typeparam>
        /// <returns>Patron repositorio orientado a la entidad</returns>
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ClaseBase;
        /// <summary>
        /// Completa todas las transacciones con un saveChanges
        /// </summary>
        /// <returns></returns>
        Task<int> Complete();
    }
}
