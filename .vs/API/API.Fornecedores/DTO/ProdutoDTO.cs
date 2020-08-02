﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Fornecedores.DTO
{
    public class ProdutoDTO
    {
        [Key]
        public Guid Id { get; set; }
        public Guid FornecedorId { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string Descricao { get; set; }
        public string ImagemUpload { get; set; }
        public string Imagem { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public string NomeFornecedores { get; set; }
    }
}
