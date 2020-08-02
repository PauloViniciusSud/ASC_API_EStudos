using System;
using System.Collections.Generic;
using System.Linq;
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
        IFornecedorRepository _fornecedorRepository;
        IMapper _mapper;
        public FornecedoresController(
            IFornecedorRepository fornecedorRepository,
            IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>>ObterRetornar()
            {

            var Fornecedores = _mapper.Map<IEnumerable<FornecedorDTO>>(await _fornecedorRepository.ObterTodos());
            return Ok(Fornecedores);

            }
    }
}
