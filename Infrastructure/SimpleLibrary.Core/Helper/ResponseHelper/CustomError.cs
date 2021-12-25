namespace SimpleLibrary.Core.Helper.ResponseHelper;

public class CustomError
{
    public int Code { get; set; }
    public string Message { get; set; }

    public CustomError(int code) => this.Code = code;
    public CustomError()
    {
    }

    public CustomError(int code, string message)
        : this(message)
    {
        this.Code = code;
    }

    public CustomError(string message) => this.Message = message;

}