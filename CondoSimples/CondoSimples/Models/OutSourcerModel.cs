using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class OutSourcerModel
    {
        public int ID { get; set; }
        [Display(Name = "Nome do prestador")]
        [Required]
        public string CompanyName { get; set; }
        [Display(Name = "Telefone")]
        [Required]
        public string Tel { get; set; }
        [Display(Name = "CNPJ")]
        [Required]
        public string CNPJ { get; set; }

        [Display(Name = "Url do site")]
        public string Site { get; set; }
        [Display(Name = "Referência de contato")]
        public string Contact { get; set; }

        public AddressModel Address { get; set; }
        public CondoModel Condo { get; set; }
    }
}
