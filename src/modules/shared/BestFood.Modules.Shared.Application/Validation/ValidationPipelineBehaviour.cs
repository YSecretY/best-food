using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BestFood.Modules.Shared.Application.Validation;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken))
        );

        var errors = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationFailure
            (
                validationFailure.PropertyName,
                validationFailure.ErrorMessage
            ))
            .ToList();

        if (errors.Count is not 0)
            throw new ValidationException(errors);

        var response = await next();

        return response;
    }
}