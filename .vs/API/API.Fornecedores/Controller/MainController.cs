using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Fornecedores.Controller
{
    //é necessário injectar o Inotificador
    
    
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;

        public MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        //Validacao de notificacoes de erro
        protected ActionResult CustomResponse(ModelStateDictionary modelStateDictionary)   
        {
            if (!ModelState.IsValid)
                NotificarErroModelInvalida(modelStateDictionary);
            return CustomResponse();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {

                    Sucess = true,
                    Data = result
                });
            }
            else
            {
                return BadRequest(new
                {

                    Sucess = false,
                    erros = _notificador.ObterNotificacoes().Select(m => m.Mensagem)
                });
            }
            
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            //Selecionar os erros.
            
            //Os erros estão em varias coleções e precisamos percorrer uma a uma.
            var erros = modelState.Values.SelectMany(p => p.Errors);
            foreach(var erro in erros)
            {
                //Verificar se o erro foi um exception ou não
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        public void NotificarErro(string errorMsg)
        {
            //Objeto responsável pela notificação, que será lançado.

            _notificador.Handle(new DevIO.Business.Notificacoes.Notificacao(errorMsg));
        }
        //Validacao de modoStates

        //Validacao de operacao de negocio
    }
}
