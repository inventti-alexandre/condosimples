using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        [Display(Name = "Notificação")]
        [Required]
        public string Message { get; set; }
        [Display(Name = "Data de registro")]
        public DateTime DateRegister { get; set; }
        public UserModel User { get; set; }
        public CondoModel Condo { get; set; }
    }
}
