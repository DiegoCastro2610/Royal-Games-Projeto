using RoyalGames.Dtos.UsuarioDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Models;
using System.Security.Cryptography;
using System.Text;

namespace RoyalGames.Applications.Service
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            LerUsuarioDto lerUsuario = new LerUsuarioDto
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Email = usuario.Email,
                StatusUsuario = usuario.StatusUsuario
            };

            return lerUsuario;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<LerUsuarioDto> ListaUsuarioDto = usuarios.Select(usuarioDb => LerDto(usuarioDb)).ToList();
            return ListaUsuarioDto;
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                throw new DomainException("Email invalido");
            }
        }

        private static byte[] HashSenha(string  senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha é obrigatoria");
            }
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDto ObterPorId(int id)
        {
            Usuario usuario = _repository.ObterPorId(id);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao existe");
            }

            return LerDto(usuario);
        }

        public LerUsuarioDto ObterPorEmail(string email)
        {
            Usuario usuario = _repository.ObterPorEmail(email);

            if (usuario == null)
            {
                throw new DomainException("Usuario nao Existe ou Email errado");
            }

            return LerDto(usuario);
        }

    public LerUsuarioDto Adicionar(CriarUsuarioDto usuarioDto)
        {
            ValidarEmail(usuarioDto.Email);

            if (_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainException("Existe esse email já");
            }

            Usuario usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha,
                StatusUsuario = true
            };

            return LerDto(usuario);
        }

        public LerUsuarioDto Atualizar(int id, CriarUsuarioDto usuarioDto)
        {
            ValidarEmail(usuarioDto.Email);

            Usuario usuarioDb = _repository.ObterPorId(id);

            if (usuarioDb == null)
            {
                throw new DomainException("Usuario não encontrado");
            }

            usuarioDb.Nome = usuarioDto.Nome;
            usuarioDb.Email = usuarioDto.Email;
            usuarioDb.Senha = usuarioDto.Senha;

            _repository.Atualizar(usuarioDb);

            return LerDto(usuarioDb);
        }

        public void Remover(int id)
        {
            Usuario usuario = _repository.ObterPorId(id);

            if(usuario == null)
            {
                throw new DomainException("Usuario não existe");
            }

            _repository.Remover(id);
        }
    }
}
