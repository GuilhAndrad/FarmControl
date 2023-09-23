using FarmControl.Communication.Response;
using FarmControl.Exceptions;
using FarmControl.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MeuLivroDeReceitas.Api.Filters;

public class ExceptionFilters : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is FarmControlException)
        {
            HandleMeuLivroDeReceitasExceptions(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }
    private static void HandleMeuLivroDeReceitasExceptions(ExceptionContext context)
    {
        if (context.Exception is ValidationErrorsException)
        {
            HandleValidationErrorsException(context);
        }
        else if (context.Exception is InvalidLoginException)
        {
            HandleLoginException(context);
        }
    }

    private static void HandleValidationErrorsException(ExceptionContext context)
    {
        var errorValidationRequest = context.Exception as ValidationErrorsException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ErrorResponseJson(errorValidationRequest.ErrorMensages));
    }

    private static void ThrowUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ErrorResponseJson(ResourceMensagesError.ERRO_DESCONHECIDO));
    }

    private static void HandleLoginException(ExceptionContext context)
    {
        var errorLogin = context.Exception as InvalidLoginException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new ErrorResponseJson(errorLogin.Message));
    }
}