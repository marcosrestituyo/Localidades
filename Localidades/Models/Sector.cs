using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Localidades.Models
{
    public class Sector
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sector")]
        public string Nombre { get; set; }
        [Display(Name = "Cantidad de Calles")]
        public int? NumeroCalles { get; set; }

        [Required]
        [Display(Name = "Seccion a la que pertenece")]
        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }
    }
}