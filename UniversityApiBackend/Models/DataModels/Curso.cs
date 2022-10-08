using System.ComponentModel.DataAnnotations;
using UniversityApiBackend.Enum;

namespace UniversityApiBackend.Models.DataModels
{
    public class Curso
    {
        [Required, StringLength(50)]
        public string Nombre { get; set; }

        [Required, StringLength(280)]
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }
        public string PublicoObjetivo { get; set; }
        public string Objetivos { get; set; }
        public string Requisitos { get; set; }
        public Nivel Nivel { get; set; }
    }
}
