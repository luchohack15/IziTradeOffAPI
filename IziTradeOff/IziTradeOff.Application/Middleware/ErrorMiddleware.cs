using IziTradeOff.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IziTradeOff.Application.Middleware
{
    public class ErrorMiddleware : IMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorMiddleware> logger;
        /// <summary>
        /// Contructor para injeccion de dependencias
        /// </summary>
        /// <param name="_next">Nuevo request</param>
        /// <param name="_logger">Manejador de errores</param>
        /// Johnny Arcia
        public ErrorMiddleware(RequestDelegate _next, ILogger<ErrorMiddleware> _logger)
        {
            next = _next;
            logger = _logger;
        }

        /// <summary>
        /// Contructor para injeccion de dependencias
        /// </summary>
        /// <param name="_logger">Manejador de errores</param>
        /// Johnny Arcia
        public ErrorMiddleware(ILogger<ErrorMiddleware> _logger) => logger = _logger;

        /// <summary>
        /// Metodo publico con el que se dispara la excepcion
        /// </summary>
        /// <param name="context">Contexto del status http</param>
        /// <returns>Retorna una tarea async</returns>
        /// Johnny Arcia
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //Si la tarea se termina con exito, continua
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                //Si ocurre un error se dispara una nueva tarea con el error del middleware
                await ManejadorExceptionAsincrono(context, ex, logger);
            }
        }

        /// <summary>
        /// Metodo publico con el que se dispara la excepcion
        /// </summary>
        //// <param name="context">Contexto del status http</param>
        /// <param name="next">Nuevo request</param>
        /// <returns>Retorna una tarea async</returns>
        /// Johnny Arcia
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                await ManejadorExceptionAsincrono(context, ex, logger);
            }
        }

        /// <summary>
        /// Metodo encargado de generar y manejar las excepciones
        /// </summary>
        /// <param name="context">Contexto del status http</param>
        /// <param name="ex">Excepcion</param>
        /// <param name="logger">Manejador de errores</param>
        /// <returns>Nueva tarea con el error</returns>
        /// Johnny Arcia
        private async Task ManejadorExceptionAsincrono(HttpContext context, Exception ex, ILogger<ErrorMiddleware> logger)
        {
            object errores = null;
            int statusCode = GetStatusCode(ex);
            var response = new
            {
                title = GetTitle(ex),
                status = statusCode,
                detail = ex.Message,
                errors = GetErrors(ex)
            };
            //Se evalua el tipo de error
            switch (ex)
            {
                //Excepcion base controlada
                case ExceptionBase me:
                    logger.LogError(ex, "Manejador Error");
                    errores = response;
                    context.Response.StatusCode = (int)me.codigo;
                    break;
                //Excepcion no controlada
                case Exception e:
                    logger.LogError(ex, GetTitle(e));
                    errores = response;
                    context.Response.StatusCode = statusCode;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                var resultado = JsonConvert.SerializeObject(new { errores });
                //Se genera un nuevo proceso http
                await context.Response.WriteAsync(resultado);
            }

        }
        /// <summary>
        /// Obtiene el codigo de la respuesta del servidor segun la excepcion
        /// </summary>
        /// <param name="exception">objeto Exception</param>
        /// <returns>Numero codigo de la excepcion</returns>
        /// Johnny Arcia
        private static int GetStatusCode(Exception exception) => exception switch
        {
            BadRequestException _=> StatusCodes.Status400BadRequest,
            NotFoundException _=> StatusCodes.Status404NotFound,
            ValidationException _=> StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

        /// <summary>
        /// Obtiene el titulo de la excepcion capturada
        /// </summary>
        /// <param name="exception">objeto Exception</param>
        /// <returns>Titulo de la excepcion</returns>
        /// Johnny Arcia
        private static string GetTitle(Exception exception) => exception switch
        {
            IziTradeOff.Application.Exceptions.ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

        /// <summary>
        /// Obtiene los errores capturados en un diccionario
        /// </summary>
        /// <param name="exception">objeto Exception</param>
        /// <returns>Diccionario con los errores</returns>
        /// Johnny Arcia
        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }

            return errors;
        }
    }
}
