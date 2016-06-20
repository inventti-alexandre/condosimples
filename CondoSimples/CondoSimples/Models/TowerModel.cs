using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class TowerModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Condo_ID { get; set; }
        public List<UnitModel> Units { get; set; }

        [ForeignKey("Condo_ID")]
        public CondoModel Condo { get; set; }
    }
}
