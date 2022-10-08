using UniversityApiBackend.Enum;

namespace UniversityApiBackend.Models.DataModels
{
    public class Curso
    {
        public string Nombre { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }
        public string PublicoObjetivo { get; set; }
        public string Objetivos { get; set; }
        public string Requisitos { get; set; }
        public Nivel Nivel { get; set; }
    }
}
