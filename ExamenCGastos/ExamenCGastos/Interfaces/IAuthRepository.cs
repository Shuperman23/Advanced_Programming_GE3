namespace ExamenCGastos.Interfaces
{
    public interface IAuthRepository
    {
        Task<string?> AuthenticateAsync(string correo, string clave);
    }
}