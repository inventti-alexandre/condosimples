using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class BoardModel
    {
        public int Id { get; set; }
        public string Post { get; set; }
        public DateTime DatePost { get; set; }
        public DateTime DateExpires { get; set; }
        public ApplicationUser User { get; set; }
    }
}
