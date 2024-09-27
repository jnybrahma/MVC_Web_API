using System.ComponentModel.DataAnnotations;

namespace DevaLokaVillaAPI.Models.Dto
{
    public class VillaNumberDTO
    {
        public int Id { get; set; }

        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string SpecialDetails { get; set; }= String.Empty;

        public VillaDTO Villa { get; set; }



    }
}
