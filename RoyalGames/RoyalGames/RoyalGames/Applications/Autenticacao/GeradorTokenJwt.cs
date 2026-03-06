using Microsoft.IdentityModel.Tokens;
using RoyalGames.Exceptions;
using RoyalGames.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RoyalGames.Applications.Autenticacao
{
    public class GeradorTokenJwt
    {
        private readonly IConfiguration _config;

        public GeradorTokenJwt(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Usuario usuario)
        {
            // key -> chave secreta usada para assinar o token
            // garante que o token nao foi alterado
            var chave = _config["Jwt:key"]!;

            // ISSUER -> quem gerou o token (nome da API / sistema que gerou)
            // a API valida se o token veio do emissor correto
            var issuer = _config["Jwt:Issuer"]!;

            // AUDIENCE -> para quem o token foi criado
            //define qual sistema pode usar o token
            var audience = _config["Jwt:Audience"]!;

            // TEMPO DE EXPIRACAO -> define quantos minutos o token sera valido
            // depois disso, o usuario precisa logar novamente.
            var expiraEmMinutos = int.Parse(_config["Jwt:ExpiraEmMinutos"]!);

            // Converte a chave para bytes (necessario para criar a assinatura)
            var keyBytes = Encoding.UTF8.GetBytes(chave);

            if (keyBytes.Length < 32)
            {
                throw new DomainException("Jwt: key precisa ter pelo menos 32 caracteres (256 bits).");
            }

            var securityKey = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Claims informaçoes do usuario que vao dentro do token e essas informacoes podem ser recuperadas na api para identificar quem esta logando
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()), // Id do usuario para saber quem fez acao

                 new Claim(ClaimTypes.Name, usuario.Nome), // Nome do usuario

                 new Claim(ClaimTypes.Email, usuario.Email) // Email do usuario
            };

            // Cria o token jwt com todas as informacoes 
            var token = new JwtSecurityToken(
            issuer: issuer,                                     // quem gerou o tokenm
            audience: audience,                                 // quem pode usar o token
            claims: claims,                                     // dados do usuario
            expires: DateTime.Now.AddMinutes(expiraEmMinutos),  // validade do token
            signingCredentials: credentials                    //  assinatura de segurança
        );

            return new JwtSecurityTokenHandler().WriteToken(token); // Converte o token para string e essa string é enviada para o cliente
        }
    };
}

