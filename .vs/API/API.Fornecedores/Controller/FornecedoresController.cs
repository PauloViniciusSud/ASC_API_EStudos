using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using API.Fornecedores.DTO;
using AutoMapper;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Fornecedores.Controller
{
    [Route("api/fornecedores")]
    [ApiController]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        IMapper _mapper;
        public FornecedoresController(
            IFornecedorRepository fornecedorRepository,
            IMapper mapper,
            IFornecedorService fornecedorService)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>>ObterRetornar()
        {

            var Fornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(await _fornecedorRepository.ObterTodos());
            return Ok(Fornecedores);

        }

        [HttpGet("ObterPorId/{id:guid}")]
        public async Task<ActionResult<FornecedorDTO>>ObterPorId(Guid id)
        {
            if(id == null)
            {
                return BadRequest("é necessário informar um Id");
            }


            var Fornecedor = _mapper.Map<FornecedorDTO>(await _fornecedorRepository.ObterPorId(id));
            
            if(Fornecedor == null)
            {
                return BadRequest("Não existe usuário com  {0} informado, por favor reveja a solicitação");
            }
            
            return Fornecedor;
        }
        [HttpGet("ObterFornecedorProdutosEndereco/{id:guid}")]
        public async Task<ActionResult<FornecedorDTO>>ObterFornecedorProdutosEndereco(Guid id)
        {
            if (id == null)
            {
                return NotFound("é necessário informar um Id");
            }


            var Fornecedor = _mapper.Map<FornecedorDTO>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));

            if (Fornecedor == null)
            {
                return BadRequest("Não existe usuário com  {0} informado, por favor reveja a solicitação");
            }
            
            return Fornecedor;
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorDTO>> Post(FornecedorDTO fornecedor)
        {
            //O ModelState é conferido pela viewmodel
            if(ModelState.IsValid)
            {
                return BadRequest();
            }

            if(fornecedor == null)
            {
                return NotFound();
            }
            var novoFornecedor = _mapper.Map<Fornecedor>(fornecedor);
            var resultado = await _fornecedorService.Adicionar(novoFornecedor);

            if (!resultado) return BadRequest();
            else
            {
                return Ok("O fornecedor " + fornecedor.Nome + " foi criado com sucesso");
            }
        }

        [HttpPut("{guid:id}")]
        public async Task<ActionResult<FornecedorDTO>>Put(Guid id, FornecedorDTO fornecedorDTO)
        {
            if (fornecedorDTO.Id != id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var Novofornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);

            Task<bool> resultado =  _fornecedorService.Atualizar(Novofornecedor);

            var converterConvertido = Convert.ToBoolean(resultado);

            if (!converterConvertido) return BadRequest();

            return Ok(Novofornecedor);


        }

        [HttpDelete("{guid:id}")]
        public async Task<ActionResult<FornecedorDTO>>Delete(Guid id)
        {
            if (id == null) return NotFound("O id não foi informado");

            var fornecedor = await _fornecedorRepository.ObterPorId(id);

            if (fornecedor == null) return BadRequest("Id não localizado");

            var resultado = await _fornecedorService.Remover(id);

            if (!resultado) return BadRequest("Não possivel deletar o usuário informado");

            return Ok("O usuário " + fornecedor.Nome + " Foi deletado com sucesso");

        }


    }
}
