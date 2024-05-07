namespace PheasantBench.Application.Abstractions
{
    public interface ITokenService
    {
        string GetUsername(string token);
    }
}
