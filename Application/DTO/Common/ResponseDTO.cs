using MediatR;

namespace Application.DTO.Common;

public class ResponseDTO<T> : IRequest<Unit>
{
    public ResponseDTO() { }

    public ResponseDTO(T data)
    {
        this.Data = data;
    }

    public ResponseDTO(string msg)
    {
        this.Message = msg;
    }

    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; }
}