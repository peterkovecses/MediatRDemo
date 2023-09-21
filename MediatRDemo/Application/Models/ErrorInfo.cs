namespace MediatRDemo.Application.Models;

public class ErrorInfo
{
    public string Code { get; }
    public IEnumerable<string> Message { get; }
    public object[] Args { get; }

    public ErrorInfo(string code, IEnumerable<string> message, params object[] args)
    {
        Code = code;
        Message = message;
        Args = args;
    }
}
