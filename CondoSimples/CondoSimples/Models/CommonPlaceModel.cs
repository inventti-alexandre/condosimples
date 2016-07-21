using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class CommonPlaceModel
    {
        public int ID { get; set; }
        [Display(Name = "Área comum")]
        public string Name { get; set; }
        [Display(Name = "Valor")]
        public float Price { get; set; }
        [Display(Name = "Capacidade")]
        public int Capacity { get; set; }
        [Display(Name = "Itens inclusos")]
        public string IncludeItens { get; set; }
        [Display(Name = "Pronto para locação")]
        public bool Active { get; set; }
        [Display(Name = "Termos de uso")]
        public string Terms { get; set; }

        public CondoModel Condo { get; set; }
    }
}
