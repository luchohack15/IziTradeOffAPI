using IziTradeOff.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IziTradeOff.Application.Interfaces
{
    public interface IGenericRepository<T> where T : ClaseBase
    {
        /// <summary>
        /// Buscar por Id
        /// </summary>
        /// <param name="id">Id de la entidad</param>
        /// <returns>Registro encontrado</returns>
        /// Johnny Arcia
        Task<T> GetByIdAsync(int id);
        /// <summary>
        /// Obtener todos los registros de la entidad
        /// </summary>
        /// <returns>Lista de registros sin tracking</returns>
        /// Johnny Arcia
        Task<IReadOnlyList<T>> GetAllAsync();
        /// <summary>
        /// Obtiene el primer registro encontrado
        /// </summary>
        /// <param name="spec">Query con los filtros</param>
        /// <returns>Primer Registro filtrado</returns>
        /// Johnny Arcia
        IQueryable<T> ApplySpecification(ISpecification<T> spec);
        Task<T> GetByIdWithSpec(ISpecification<T> spec);
        /// <summary>
        /// Obtiene todos los regristros que aplican al filtro
        /// </summary>
        /// <param name="spec">Query de filtros</param>
        /// <returns>Todos los registros que aplican al filtro</returns>
        /// Johnny Arcia
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
        /// <summary>
        /// Retorna la cantidad de registros con query compuesta
        /// </summary>
        /// <param name="spec">Configuracion del query</param>
        /// <returns>Cantidad de registros</returns>
        /// Johnny Arcia
        Task<int> CountAsync(ISpecification<T> spec);
        /// <summary>
        /// Agrega un nuevo registro de la entidad y aplica cambios
        /// </summary>
        /// <param name="entity">Entidad</param>
        /// <returns>Cantidad almacenada</returns>
        /// Johnny Arcia
        Task<int> Add(T entity);
        /// <summary>
        /// Actualiza un registro de entidad y aplica cambios
        /// </summary>
        /// <param name="entity">Entidad con las modificaciones</param>
        /// <returns>cantidad afectada</returns>
        /// Johnny Arcia
        Task<int> Update(T entity);
        /// <summary>
        /// Agrega un registro de la entidad sin aplicar cambios
        /// </summary>
        /// <param name="Entity">Entidad</param>
        /// Johnny Arcia
        void AddEntity(T Entity);
        /// <summary>
        /// Actualiza un registro de entidad y sin aplicar cambios
        /// </summary>
        /// <param name="Entity">Entidad con las modificaciones</param>
        /// Johnny Arcia
        /// Johnny Arcia
        void UpdateEntity(T Entity);
        /// <summary>
        /// Elimina regristro sin aplicar cambios
        /// </summary>
        /// <param name="Entity">Entidad a eliminar</param>
        void DeleteEntity(T Entity);
        /// <summary>
        /// Aplica un borrado logico de la entidad
        /// </summary>
        /// <param name="Entity">Entidad</param>
        /// Johnny Arcia
        void LogicDeleteEntity(T Entity);

    }
}
