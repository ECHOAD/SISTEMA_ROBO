using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Robos_DOM.Models
{
    public class PersonaRoboModel
    {
   
      
        public int Id { get; set; }
        [Required]
        [StringLength(12)]
        public string Cedula { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public double? Valor { get; set; }
        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required]
        public DateTime? Fecha { get; set; }

        [Required]
        public double Latitud { get; set; }
        [Required]
        public double Longitud { get; set; }
    }
}
