using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Localidades.Models
{
    public class Region
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Región")]
        public string Nombre { get; set; }
        [Display(Name = "Cantidad de Provincias")]
        public int? NumeroProvincias { get; set; }

        [Required]
        [Display(Name = "Pais al que pertenece")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}