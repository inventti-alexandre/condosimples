using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class TowerModel
    {
        public int ID { get; set; }
        [Display(Name = "Torre")]
        public string Name { get; set; }
        [Display(Name = "Total de unidades")]
        public int UnitsQtd { get; set; }
        [Display(Name = "Unidades")]
        public List<UnitModel> Units { get; set; }

        public CondoModel Condo { get; set; }
    }
}
