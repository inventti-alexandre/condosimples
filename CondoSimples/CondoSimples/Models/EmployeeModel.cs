using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class EmployeeModel
    {
        public int ID { get; set; }
        [Display(Name="Nome")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Celular")]
        public string Cel { get; set; }
        [Display(Name="Função")]
        [Required]
        public string Position { get; set; }
        [Display(Name = "Dias de trabalho")]
        public string DutyDays { get; set; }
        [Display(Name = "Horário de trabalho")]
        public string WorkShift { get; set; }

        public ApplicationUser User { get; set; }
    }
}
