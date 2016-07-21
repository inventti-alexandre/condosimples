using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class UnitModel
    {
        public int ID { get; set; }
        [Display(Name="Unidade")]
        public string Name { get; set; }
        public int Tower_ID { get; set; }

        [ForeignKey("Tower_ID")]
        public TowerModel Tower { get; set; }
    }
}
