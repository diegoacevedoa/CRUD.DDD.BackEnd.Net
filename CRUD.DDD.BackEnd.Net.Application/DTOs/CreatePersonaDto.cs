using System.ComponentModel.DataAnnotations;

namespace CRUD.DDD.BackEnd.Net.Application.DTOs
{
    public class CreatePersonaDto
    {
        [Required(ErrorMessage = "El campo NoDocumento es requerido.")]
        [StringLength(50)]
        public string NoDocumento { get; set; }

        [Required(ErrorMessage = "El campo Nombres es requerido.")]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es requerido.")]
        [StringLength(100)]
        public string Apellidos { get; set; }
    }
}
