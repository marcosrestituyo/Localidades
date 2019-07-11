using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Localidades.Models
{
    public class DistritoMunicipal
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Distrito Municipal")]
        public string Nombre { get; set; }
        [Display(Name = "Cantidad de Secciones")]
        public int? NumeroSeccion { get; set; }

        [Required]
        [Display(Name = "Municipio al que pertenece")]
        public int MunicipioId { get; set; }
        public Municipio Municipio { get; set; }
    }
}