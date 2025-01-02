using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Blockchain.Api.ExceptionHandlers;

public sealed class GlobalExceptionHandler(
    IHostEnvironment env,
    ILogger<GlobalExceptionHandler> logger
) : IExceptionHandler
{
    private const string UnhadledExceptionMsg = "An unhandled exception has occurred";

    private static readonly JsonSerializerOptions _serializerOptions = new(
        JsonSerializerDefaults.Web
    )
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
    };

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken = default
    )
    {
        logger.LogError(exception, exception.Message);
        var problemDetails = CreateProblemDetails(context, exception);
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsync(ToJson(problemDetails), cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails(in HttpContext context, in Exception exception)
    {
        var statusCode = context.Response.StatusCode;
        var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
        if (string.IsNullOrEmpty(reasonPhrase))
        {
            reasonPhrase = UnhadledExceptionMsg;
        }
        var problemDetails = new ProblemDetails { Status = statusCode, Title = reasonPhrase };
        if (!env.IsDevelopment())
            return problemDetails;

        problemDetails.Detail = exception.Message;
        problemDetails.Extensions["data"] = exception.Data;
        return problemDetails;
    }

    private string ToJson(in ProblemDetails problemDetails)
    {
        try
        {
            return JsonSerializer.Serialize(problemDetails, _serializerOptions);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception occurred while serializing the problem details");
        }
        return string.Empty;
    }
}
