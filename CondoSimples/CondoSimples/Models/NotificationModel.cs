using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateRegister { get; set; }
        public UserModel User { get; set; }
        public CondoModel Condo { get; set; }
    }
}
