using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CondoSimples.Models
{
    public class BoardModel
    {
        public int Id { get; set; }
        [Display(Name = "Publicação")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Post { get; set; }

        [Display(Name = "Data de publicação")]
        public DateTime DatePost { get; set; }
        [Display(Name = "Data de expiração")]
        public DateTime DateExpires { get; set; }
        [Display(Name = "Publicado")]
        public bool Published { get; set; }
        [Display(Name = "Usuário")]
        public ApplicationUser User { get; set; }
    }
}
