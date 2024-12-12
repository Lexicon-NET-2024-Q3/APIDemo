﻿using Domain.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Companies.API.Extensions;

public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {

                var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeatures != null) 
                {
                    var problemDetailsFactory = app.Services.GetService<ProblemDetailsFactory>();
                    ArgumentNullException.ThrowIfNull(nameof(problemDetailsFactory));

                    var problemDetails = CreateProblemDetails(context, contextFeatures.Error, problemDetailsFactory, app);

                    context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(problemDetails);
                }
            });
        });
    }

    private static ProblemDetails CreateProblemDetails(HttpContext context, Exception error, ProblemDetailsFactory problemDetailsFactory, WebApplication app)
    {
        return error switch
        {
            CompanyNotFoundException companyNotFoundException => problemDetailsFactory.CreateProblemDetails(
                context,
                StatusCodes.Status404NotFound,
                title: companyNotFoundException.Title,
                detail: companyNotFoundException.Message,
                instance: context.Request.Path),


            _ => problemDetailsFactory.CreateProblemDetails(
                context,
                StatusCodes.Status500InternalServerError,
                title: "Internal Server Error",
                detail: app.Environment.IsDevelopment() ? error.Message : "An unexpected error occurred.")
        };
    }
}
