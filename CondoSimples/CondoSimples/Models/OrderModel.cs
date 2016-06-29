using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class OrderModel
    {
        public int ID { get; set; }
        public EmployeeModel UserEmployee { get; set; }
        public UserModel UserRecipient { get; set; }
        public DateTime DateReceived { get; set; }
        public string Description { get; set; }
    }
}
