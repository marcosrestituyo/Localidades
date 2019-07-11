using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Localidades.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Provincia")]
        public string Nombre { get; set; }
        [Display(Name = "Cantidad de Municipios")]
        public int? NumeroMunicipios { get; set; }

        [Required]
        [Display(Name = "Región a la que pertenece")]
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}