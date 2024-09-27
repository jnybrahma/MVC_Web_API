using DevaLokaVillaAPI.Models.Dto;

namespace DevaLokaVillaAPI.Data
{
    public class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO> {
                new VillaDTO{ Id=1, Name="Pool View", Sqft=111, Occupancy=2},
                new VillaDTO{ Id =2, Name="Beach View", Sqft=222, Occupancy=3},
                 new VillaDTO{ Id =3, Name="Natures View", Sqft=333, Occupancy=4}
            };
    }
}
