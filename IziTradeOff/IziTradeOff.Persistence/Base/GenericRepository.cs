using IziTradeOff.Application.Interfaces;
using IziTradeOff.Domain;
using IziTradeOff.Persistence.Connection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IziTradeOff.Persistence.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ClaseBase
    {
        /// <summary>
        /// Contexto de la BD
        /// </summary>
        private readonly IConexion _context;
        /// <summary>
        /// Constructor del repositorio
        /// </summary>
        /// <param name="context">Contexto</param>
        public GenericRepository(IConexion context)
        {
            _context = context;
        }
        /// <summary>
        /// Obtener todos los registros de la entidad
        /// </summary>
        /// <returns>Lista de registros sin tracking</returns>
        /// Johnny Arcia
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        /// <summary>
        /// Buscar por Id
        /// </summary>
        /// <param name="id">Id de la entidad</param>
        /// <returns>Registro encontrado</returns>
        /// Johnny Arcia
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        /// <summary>
        /// Obtiene el primer registro encontrado
        /// </summary>
        /// <param name="spec">Query con los filtros</param>
        /// <returns>Primer Registro filtrado</returns>
        /// Johnny Arcia
        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Obtiene todos los regristros que aplican al filtro
        /// </summary>
        /// <param name="spec">Query de filtros</param>
        /// <returns>Todos los registros que aplican al filtro</returns>
        /// Johnny Arcia
        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        /// <summary>
        /// Retorna query compuesta de querys 
        /// </summary>
        /// <param name="spec">Configuracion del query</param>
        /// <returns>Query con la configuracion aplicada</returns>
        /// Johnny Arcia
        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
        /// <summary>
        /// Retorna la cantidad de registros con query compuesta
        /// </summary>
        /// <param name="spec">Configuracion del query</param>
        /// <returns>Cantidad de registros</returns>
        /// Johnny Arcia
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        /// <summary>
        /// Agrega un nuevo registro de la entidad y aplica cambios
        /// </summary>
        /// <param name="entity">Entidad</param>
        /// <returns>Cantidad almacenada</returns>
        /// Johnny Arcia
        public async Task<int> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Actualiza un registro de entidad y aplica cambios
        /// </summary>
        /// <param name="entity">Entidad con las modificaciones</param>
        /// <returns>cantidad afectada</returns>
        /// Johnny Arcia
        public async Task<int> Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Agrega un registro de la entidad sin aplicar cambios
        /// </summary>
        /// <param name="Entity">Entidad</param>
        /// Johnny Arcia
        public void AddEntity(T Entity)
        {
            _context.Set<T>().Add(Entity);
        }
        /// <summary>
        /// Actualiza un registro de entidad y sin aplicar cambios
        /// </summary>
        /// <param name="Entity">Entidad con las modificaciones</param>
        /// Johnny Arcia
        public void UpdateEntity(T Entity)
        {
            _context.Set<T>().Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
        }
        /// <summary>
        /// Elimina regristro sin aplicar cambios
        /// </summary>
        /// <param name="Entity">Entidad a eliminar</param>
        public void DeleteEntity(T Entity)
        {
            _context.Set<T>().Remove(Entity);
        }
        /// <summary>
        /// Aplica un borrado logico de la entidad
        /// </summary>
        /// <param name="Entity">Entidad</param>
        /// Johnny Arcia
        public void LogicDeleteEntity(T Entity)
        {
            Entity.Estado = false;
        }
    }
}