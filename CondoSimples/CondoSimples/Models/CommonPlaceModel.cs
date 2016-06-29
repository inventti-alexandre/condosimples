using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class CommonPlaceModel
    {
        public int ID { get; set; }
        [Display(Name = "Área comum")]
        public string Name { get; set; }
        public int MyProperty { get; set; }
    }
}
