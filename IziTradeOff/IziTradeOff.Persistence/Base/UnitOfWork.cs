using IziTradeOff.Application.Interfaces;
using IziTradeOff.Domain;
using IziTradeOff.Persistence.Connection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IziTradeOff.Persistence.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Representa el hash code de las entidades
        /// </summary>
        private Hashtable _repositories;
        /// <summary>
        /// Contexto
        /// </summary>
        private readonly IConexion _context;
        /// <summary>
        /// constructor con las dependencias
        /// </summary>
        /// <param name="context">Contexto</param>
        public UnitOfWork(IConexion context)
        {
            _context = context;
        }
        /// <summary>
        /// Completa las transacciones y guarda los cambios
        /// </summary>
        /// <returns>cantidad de registros afectados</returns>
        /// Johnny Arcia
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Se cierra la conexion del contexto
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
        /// <summary>
        /// Aplica el patron repositorio a la entidad
        /// </summary>
        /// <typeparam name="TEntity">Entidad</typeparam>
        /// <returns>Patron repositorio aplicado a la entidad</returns>
        /// Johnny Arcia
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ClaseBase
        {
            //Aplica el codigo hash de la entidad
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;
            //Se busca las propiedades de navegacion de la entidad
            if (!_repositories.ContainsKey(type))
            {
                //Se crea la instancia para el repositorio generico
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
