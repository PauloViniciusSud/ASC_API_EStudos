using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Fornecedores.DTO;
using AutoMapper;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Business.Notificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Fornecedores.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : MainController
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;
        public EnderecosController(IEnderecoRepository enderecoRepository,
                                   INotificador notificador,
                                   IMapper mapper): base(notificador)
        {
            _enderecoRepository = enderecoRepository;
            _notificador = notificador;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoDTO>>>ObterTodos()
        {
            var enderecos =   _mapper.Map<IEnumerable<EnderecoDTO>>(await _enderecoRepository.ObterTodos());

            return Ok(enderecos);
        }

        [HttpGet]
        public async Task<ActionResult<EnderecoDTO>>SelecionarPorId(Guid Id)
        {
            if (Id == null)
                return NotFound("Página não encontrada");
            var Endereco =  _mapper.Map <EnderecoDTO>(await _enderecoRepository.ObterPorId(Id));
            if (Endereco == null)
                return CustomResponse(ModelState);
            return Ok(Endereco);
        }


        [HttpPut]
        public async Task<ActionResult<EnderecoDTO>>Alterar(EnderecoDTO endereco)
        {
            if (endereco == null)
                return NotFound("Página não encontrada");
            var retorno = VerificarSeEnderecoExiste(endereco.Id);
            if (!retorno)
                BadRequest("O usuário não existe");
            await _enderecoRepository.Atualizar(_mapper.Map<Endereco>(endereco));
            return Ok("O endereço do fornecedor " +endereco.FonecedorId+ " foi atualizado com sucesso");

        }

        private bool VerificarSeEnderecoExiste(Guid id)
        {
           var Endereco =   _enderecoRepository.ObterPorId(id);

            if (Endereco == null)
                return false;
            else
                return true;
        }
       


    }
}
