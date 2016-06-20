using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        [Display(Name="CPF")]
        public string CPF { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Celular")]
        [DataType(DataType.PhoneNumber)]
        public string Cel { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Moradores")]
        public string Residents { get; set; }
        [Display(Name = "Animais de estimação")]
        public string Pets { get; set; }
        [Display(Name = "Carros")]
        public string Cars { get; set; }
        [Display(Name = "Visitantes frequentes")]
        public string Visitors { get; set; }

        public int Unit_ID { get; set; }
        [ForeignKey("Unit_ID")]
        public UnitModel Unit { get; set; }

        public ApplicationUser User { get; set; }
    }
}
