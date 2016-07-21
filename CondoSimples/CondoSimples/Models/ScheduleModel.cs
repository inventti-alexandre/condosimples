using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class ScheduleModel
    {
        public int ID { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateSchedule { get; set; }

        public CommonPlaceModel Place { get; set; }
        public UserModel User { get; set; }
    }
}
