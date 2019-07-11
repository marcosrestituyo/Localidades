using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Localidades.Models
{
    public class Local
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Local")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Calle en la que esta situada")]
        public int CalleId { get; set; }
        public Calle Calle { get; set; }
    }
}