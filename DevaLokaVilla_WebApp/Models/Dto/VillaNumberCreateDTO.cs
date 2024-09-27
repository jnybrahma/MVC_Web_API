using System.ComponentModel.DataAnnotations;

namespace DevaLokaVilla_WebApp.Models.Dto
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string SpecialDetails { get; set; } = String.Empty;



    }
}
