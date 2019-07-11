using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Localidades.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "País")]
        public string Nombre { get; set; }
        [Display(Name = "Cantidad de Regiones")]
        public int? CantidadRegiones { get; set; }
    }
}