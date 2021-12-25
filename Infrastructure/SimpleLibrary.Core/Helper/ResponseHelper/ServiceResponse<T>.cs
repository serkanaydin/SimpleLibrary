namespace SimpleLibrary.Core.Helper.ResponseHelper;
public sealed class ServiceResponse<T> : ServiceResponse
{
  public ServiceResponse(T result, bool isSuccessful = true)
    : base(isSuccessful)
  {
    this.Result = result;
  }

  public ServiceResponse(T result, CustomError error, bool isSuccessful = false)
    : base(error, isSuccessful)
  {
    this.Result = result;
  }

  public T Result { get; set; }
}