namespace Mostra.Application.Interfaces
{
    public interface IQrCodeGenerator
    {
        byte[] GeneratePng(string content);
    }
}