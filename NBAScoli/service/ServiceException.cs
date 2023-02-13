namespace NBAScoli.service;

public class ServiceException : Exception
{
    protected string Message { get; set; }
    public ServiceException(string message)
    {
        this.Message = message;
    }
}