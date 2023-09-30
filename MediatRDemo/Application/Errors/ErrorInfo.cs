namespace MediatRDemo.Application.Errors;

public class ErrorInfo
{
    public string Code { get; }
    public IEnumerable<ApplicationError> Errors { get; }

    public ErrorInfo(string code, IEnumerable<ApplicationError> errors)
    {
        Code = code;
        Errors = errors;
    }

    public static ErrorInfo NotFound(object id)
        => new(
            ErrorCodes.NotFound,
            new[]
            {
                new ApplicationError(
                    string.Format(ErrorMessages.NotFound, id),
                    new KeyValuePair<string, object>(nameof(id), id))
            });
}
