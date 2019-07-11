using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Localidades.Models
{
    public class Calle
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Calle")]
        public string Nombre { get; set; }
        [Display(Name = "Cantidad de Locales")]
        public int? NumeroLocales { get; set; }

        [Required]
        [Display(Name = "Sector al que pertenece")]
        public int SectorId { get; set; }
        public Sector Sector { get; set; }
    }
}