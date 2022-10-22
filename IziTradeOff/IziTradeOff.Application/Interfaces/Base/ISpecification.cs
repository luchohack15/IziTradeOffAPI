using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IziTradeOff.Application.Interfaces
{
    public interface ISpecification<T>
    {
        /// <summary>
        /// Aplica un criterio de busqueda en la expresion
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }
        /// <summary>
        /// Incluye objetos en las propiedades de mapeo
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }
        /// <summary>
        /// Agrega una expresion de ordenamiento en la consulta
        /// </summary>
        Expression<Func<T, object>> OrderBy { get; }
        /// <summary>
        /// Agrega una expresion de ordenamiento descendente en la consulta
        /// </summary>
        Expression<Func<T, object>> OrderByDescending { get; }
        /// <summary>
        /// Contiene la cantidad de registros a obtener
        /// </summary>
        int Take { get; }
        /// <summary>
        /// Contiene la cantidad de registros a excluir
        /// </summary>
        int Skip { get; }
        /// <summary>
        /// Habilita paginacion en los registros
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}
