namespace SimpleLibrary.Core.Helper.ResponseHelper;

public interface IResponseHelper
{
    ServiceResponse SetSuccess();

    ServiceResponse<T> SetSuccess<T>(T data);

    ServiceResponse<T> SetError<T>(
        T data,
        string errorMessage,
        int statusCode = 500,
        bool isLogging = false);

    ServiceResponse SetError(string errorMessage, int statusCode = 500, bool isLogging = false);

    ServiceResponse SetError(CustomError errorItem, bool isLogging = false);

    ServiceResponse<T> SetError<T>(T data, CustomError errorInfo, bool isLogging = false);
}