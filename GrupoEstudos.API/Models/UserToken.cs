namespace GrupoEstudos.API.Models;

public class UserToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public string Mensagem { get; set; }
    public string Nome { get; set; }
    public string Id { get; set; }
}
