namespace Baristaa.Models;

public class Result
{
    public string Message { get; set; } = "Your piping hot coffee is ready";
    public string Prepared { get; set; } = DateTime.UtcNow.ToString("O");
    public int StatusCode { get; set; } = 200;
}