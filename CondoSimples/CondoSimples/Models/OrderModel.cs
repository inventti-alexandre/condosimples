using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class OrderModel
    {
        public int ID { get; set; }
        [Display(Name = "Registrado por")]
        public EmployeeModel UserEmployee { get; set; }
        [Display(Name = "Destinatário")]
        public UserModel UserRecipient { get; set; }
        [Display(Name = "Data de recebimento")]
        [DataType(DataType.Date)]
        public DateTime DateReceived { get; set; }
        [Display(Name = "Descrição do objeto")]
        public string Description { get; set; }
    }
}
