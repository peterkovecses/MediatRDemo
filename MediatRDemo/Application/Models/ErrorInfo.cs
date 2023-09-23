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
}
