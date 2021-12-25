using System;
using Microsoft.Extensions.Logging;

namespace SimpleLibrary.Core.Helper.ResponseHelper;

public class ResponseHelper : IResponseHelper,IDisposable
{
    private readonly ILogger logger;
    private bool isDisposed;

    public ResponseHelper() {
    ILoggerFactory loggerFactory
      = LoggerFactory.Create(builder => { builder.AddConsole(); });
    logger = loggerFactory.CreateLogger<ResponseHelper>();
    }
    
    ~ResponseHelper() => this.Dispose(false);

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
    }
      public ServiceResponse<T> SetError<T>(
      T data,
      string errorMessage,
      int statusCode = 500,
      bool isLogging = false)
    {
      CustomError error = new (statusCode, errorMessage);
      if (isLogging)
        this.logger.LogError(errorMessage, (object) error);
      return new ServiceResponse<T>(data, error);
    }

    public ServiceResponse SetError(
      string errorMessage,
      int statusCode = 500,
      bool isLogging = false)
    {
      return this.SetError(new CustomError(statusCode, errorMessage),isLogging);
    }

    public ServiceResponse SetError(CustomError errorItem, bool isLogging = false)
    {
      if (isLogging)
        this.logger.LogError(errorItem?.Message, (object) errorItem);
      return new ServiceResponse(errorItem);
    }

    public ServiceResponse<T> SetError<T>(
      T data,
      CustomError errorInfo,
      bool isLogging = false)
    {
      if (isLogging)
        this.logger.LogError(errorInfo?.Message, (object) errorInfo);
      return new ServiceResponse<T>(data, errorInfo);
    }

    public ServiceResponse SetSuccess() => new ServiceResponse();

    public ServiceResponse<T> SetSuccess<T>(T data) => new ServiceResponse<T>(data);

    protected virtual void Dispose(bool disposing)
    {
      if (this.isDisposed)
        return;
      this.isDisposed = true;
      if (!disposing || !(this.logger is IDisposable logger) || logger == null)
        return;
      logger.Dispose();
    }
}