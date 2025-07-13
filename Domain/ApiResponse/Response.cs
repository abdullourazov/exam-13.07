using System.Net;

namespace Domain.ApiResponse;

public class Response<T>
{
    public bool IsSucces { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }

    public Response(T? data, string? message = null)
    {
        IsSucces = true;
        Message = message;
        Data = data;
        StatusCode = (int)HttpStatusCode.OK;
    }

    public Response(HttpStatusCode statusCode, string message)
    {
        IsSucces = false;
        Message = message;
        Data = default;
        StatusCode = (int)statusCode;
    }

    public static Response<T> Success(T? data = default, string? message = null)
    {
        return new Response<T>(data, message);
    }

    public static Response<T> Error(HttpStatusCode statusCode, string message)
    {
        return new Response<T>(statusCode, message);
    }
}
