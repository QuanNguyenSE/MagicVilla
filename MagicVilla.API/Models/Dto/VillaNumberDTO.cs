using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.Dto
{
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }

        public string SpecialDetails { get; set; }
    }
}
