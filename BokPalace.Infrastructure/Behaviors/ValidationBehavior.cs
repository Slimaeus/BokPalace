using FluentValidation;
using MediatR;
using Serilog;
using System.Text.Json;

namespace BokPalace.Infrastructure.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Log.Information(
        "[{Prefix}] Handle request={X-RequestData} and response={X-ResponseData}",
        nameof(ValidationBehavior<TRequest, TResponse>), typeof(TRequest).Name, typeof(TResponse).Name);

        Log.Debug(
            "Handled {Request} with content {X-RequestData}",
            typeof(TRequest).FullName, JsonSerializer.Serialize(request));

        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
        }

        var response = await next();

        Log.Information(
            "Handled {FullName} with content {Response}",
            typeof(TResponse).FullName, JsonSerializer.Serialize(response));

        return response;
    }
}
