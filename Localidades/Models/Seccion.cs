using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Localidades.Models
{
    public class Seccion
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sección")]
        public string Nombre { get; set; }
        [Display(Name = "Cantidad de Sectores")]
        public int? NumeroSectores { get; set; }

        [Required]
        [Display(Name = "Distrito Municipal al que pertenece")]
        public int DistritoMunicipalId { get; set; }
        public DistritoMunicipal DistritoMunicipal { get; set; }
    }
}