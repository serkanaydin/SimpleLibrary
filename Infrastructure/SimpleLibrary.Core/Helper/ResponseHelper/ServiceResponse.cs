namespace SimpleLibrary.Core.Helper.ResponseHelper;

public class ServiceResponse
{
    public ServiceResponse(bool isSuccessful = true) => this.IsSuccessful = isSuccessful;

    public ServiceResponse(CustomError error, bool isSuccessful = false)
    {
        this.Error = error;
        this.IsSuccessful = isSuccessful;
    }

    public CustomError Error { get; set; }

    public bool IsSuccessful { get; set; }
}