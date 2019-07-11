using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Localidades.Models
{
    public class Municipio
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Municipio")]
        public string Nombre { get; set; }
        [Display(Name = "Cantidad de Distritos Municipales")]
        public int? NumeroDistritoM { get; set; }

        [Required]
        [Display(Name = "Provincia a la que pertenece")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
    }
}