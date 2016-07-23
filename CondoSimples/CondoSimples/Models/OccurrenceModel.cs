using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class OccurrenceModel
    {
        public int ID { get; set; }
        [Display(Name = "Ocorrência")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Data de registro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime DateOccurrence { get; set; }
        public CondoModel Condo { get; set; }
        [Display(Name = "Usuário")]
        public UserModel User { get; set; }
        [Display(Name = "Resolvido")]
        public bool Solved { get; set; }
    }
}
