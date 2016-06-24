using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class CondoModel
    {
        public int ID { get; set; }
        [Display(Name = "Condomínio")]
        public string Name { get; set; }
        [Display(Name = "Vagas de estacionamento")]
        public int ParkingSlots { get; set; }
        [Display(Name = "Endereço")]
        public string Address { get; set; }
        [Display(Name = "Torres")]
        public List<TowerModel> Towers { get; set; }
    }
}
