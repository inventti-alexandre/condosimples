using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class BorrowModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime DateRequire { get; set; }
        public DateTime DateReturn { get; set; }
        public Nullable<DateTime> DateComplete { get; set; }

        public ApplicationUser UserRequest { get; set; }
        public ApplicationUser UserLending { get; set; }
    }
}
