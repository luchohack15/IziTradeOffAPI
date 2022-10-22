using IziTradeOff.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IziTradeOff.Persistence.Base
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public BaseSpecification() { }
        /// <summary>
        /// Constructor con la consulta especifica
        /// </summary>
        /// <param name="criteria"></param>
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        /// <summary>
        /// Variable booleana que verifica el estado de la consulta
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; }
        /// <summary>
        /// variable que contiene los objetos que se incluiran en la consulta
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        /// <summary>
        /// Agrega un objeto modelo a la consulta
        /// </summary>
        /// <param name="includeExpression">Expresion con el objeto que se debe agregar a la consulta</param>
        /// Johnny Arcia
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        /// <summary>
        /// Contiene la expresion de ordenamiento
        /// </summary>
        public Expression<Func<T, object>> OrderBy { get; private set; }
        /// <summary>
        /// Contiene la expresion de ordenamiento Descendente
        /// </summary>
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        /// <summary>
        /// Agrega expresion de ordenamiento
        /// </summary>
        /// <param name="orderByExpression">Expresion de ordenamiento</param>
        /// Johnny Arcia
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        /// <summary>
        /// Agrega expresion de ordenamiento descendente
        /// </summary>
        /// <param name="orderByDescExpression">Expresion de ordenamiento</param>
        /// Johnny Arcia
        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        /// <summary>
        /// Variable con la cantidad de registros
        /// </summary>
        public int Take { get; private set; }
        /// <summary>
        /// Variable con la cantidad de registros que debe saltar
        /// </summary>
        public int Skip { get; private set; }
        /// <summary>
        /// Variable que indica la paginacion en los resultados
        /// </summary>
        public bool IsPagingEnabled { get; private set; }
        /// <summary>
        /// Metodo interno que provoca un paginado en los registros
        /// </summary>
        /// <param name="skip">Cantidad de registros excluidos</param>
        /// <param name="take">Cantidad de registros</param>
        /// Johnny Arcia
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
