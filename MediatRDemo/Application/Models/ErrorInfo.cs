namespace MediatRDemo.Application.Models;

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
            Constants.NotFoundCode,
            new[]
            {
                new ApplicationError(
                    string.Format(Constants.NotFoundMessage, id),
                    new KeyValuePair<string, object>(nameof(id), id))
            });
}
