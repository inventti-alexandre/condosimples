﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Models
{
    public class BorrowModel
    {
        public int ID { get; set; }
        [Display(Name = "Descrição do item")]
        public string Description { get; set; }
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; } 
        [Display(Name = "Data de empréstimo")]
        [DataType(DataType.Date)]
        public DateTime DateRequire { get; set; }
        [Display(Name = "Data de devolução prevista")]
        [DataType(DataType.Date)]
        public DateTime DateReturn { get; set; }
        [Display(Name = "Data de devolução")]
        public Nullable<DateTime> DateComplete { get; set; }
        [Display(Name = "Solicitante")]
        public ApplicationUser UserRequest { get; set; }
        [Display(Name = "Concedente")]
        public ApplicationUser UserLending { get; set; }
    }
}
