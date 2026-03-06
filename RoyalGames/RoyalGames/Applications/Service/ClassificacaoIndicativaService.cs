<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using RoyalGames.Dtos.ClassificacaoIndicativaDto;
using RoyalGames.Dtos.UsuarioDto;
=======
﻿using RoyalGames.Dtos.ClassificacaoIndicativaDto;
>>>>>>> f73211678d117c562275cbecb555013fba33eeb2
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Models;
using System.Security.Cryptography;
using System.Text;

namespace RoyalGames.Applications.Service
{
    public class ClassificacaoIndicativaService
    {
        private readonly IClassificacaoIndicativa _repository;

        public ClassificacaoIndicativaService(IClassificacaoIndicativa repository)
        {
            _repository = repository;
        }

        private static LerClassificacaoIndicativaDto LerDto(ClassificacaoIndicativa classificacao)
        {
            LerClassificacaoIndicativaDto lerclassificacaodto = new LerClassificacaoIndicativaDto
            {
                ClassificaoDto = classificacao.Classificao
            };

            return lerclassificacaodto;
            
        }
        public List<LerClassificacaoIndicativaDto> Listar()
        {
            List<ClassificacaoIndicativa> classificacao = _repository.Listar();

            List<LerClassificacaoIndicativaDto> ListaClassificacaoIndicativaDto = classificacao.Select(C => LerDto(C)).ToList();
            return ListaClassificacaoIndicativaDto;
        }

        public List<LerClassificacaoIndicativaDto> Listar()
        {
            List<ClassificacaoIndicativa> classificacao = _Repository.Listar();
            List<LerClassificacaoIndicativaDto> listarclassificacaodto = classificacao.Select(C => LerDto(C)).ToList();
            return listarclassificacaodto;
        }


        public LerClassificacaoIndicativaDto ObterPorId(int id)
        {
<<<<<<< HEAD
            ClassificacaoIndicativa classificacao = _repository.ObterPorId(id);

            if (classificacao == null)
            {
                throw new DomainException("Não possui esse id");
            }

            return LerDto(classificacao);
        }





        public LerClassificacaoIndicativaDto Adicionar(CriarClassificacaoIndicativaDto ClassificacaoDto)
        {
            if (ClassificacaoDto == null)
            {
                throw new DomainException("Não existe nada escrito");
            }

            ClassificacaoIndicativa classificacao = new ClassificacaoIndicativa
            {
               Classificao = ClassificacaoDto.Classificao
            };

            _repository.Adicionar(classificacao);

            return LerDto(classificacao);
        }

        

        public void Remover(int id)
        {
            ClassificacaoIndicativa classificacao = _repository.ObterPorId(id);

            if (classificacao == null)
            {
                throw new DomainException("Usuario não existe");
            }

            _repository.Remover(id);
=======
            ClassificacaoIndicativa classificacao = _Repository.ObterPorId(id);

            if(classificacao == null)
            {
                throw new DomainException("Não Existe essa Classificação");
            }
            return LerDto(classificacao);
        }
        public LerClassificacaoIndicativaDto Adicionar(CriarClassificacaoIndicativaDto classificacaodto)
        {

            ClassificacaoIndicativa classificacao  = new ClassificacaoIndicativa
            {
                Classificao = classificacaodto.ClassificaoDto
            };

            _Repository.Adicionar(classificacao);

            return LerDto(classificacao);
        }
        public void Deletar(int id)
        {
            ClassificacaoIndicativa classificacao = _Repository.ObterPorId(id);

            if(classificacao == null)
            {
                throw new DomainException("não possui essa classificação");
            }
>>>>>>> f73211678d117c562275cbecb555013fba33eeb2
        }
    }
}
