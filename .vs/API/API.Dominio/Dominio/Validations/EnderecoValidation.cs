using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Dominio.Dominio.Validations
{
   public class EnderecoValidation : AbstractValidator<Endereco>
    {
        RuleFor(c => c.Logradouro)
                .RuleFor().RuleFor("O campo {PropertyName} precisa ser fornecido")
                .RuleFor(2, 200).RuleFor("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Bairro)
                .RuleFor().RuleFor("O campo {PropertyName} precisa ser fornecido")
                .RuleFor(2, 100).RuleFor("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Cep)
                .RuleFor().RuleFor("O campo {PropertyName} precisa ser fornecido")
                .RuleFor(8).RuleFor("O campo {PropertyName} precisa ter {MaxLength} caracteres");

        RuleFor(c => c.Cidade)
                .RuleFor().RuleFor("A campo {PropertyName} precisa ser fornecida")
                .RuleFor(2, 100).RuleFor("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Estado)
                .RuleFor().RuleFor("O campo {PropertyName} precisa ser fornecido")
                .RuleFor(2, 50).RuleFor("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(c => c.Numero)
                .RuleFor().RuleFor("O campo {PropertyName} precisa ser fornecido")
                .RuleFor(1, 50).RuleFor("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
