using DevaLokaVilla_WebApp.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevaLokaVilla_WebApp.Models.VM
{
    public class VillaNumberDeleteVM
    {
        public  VillaNumberDeleteVM()
        {
            VillaNumber = new VillaNumberDTO();
        }

        public VillaNumberDTO VillaNumber { get; set; }
        // create dropdown for villa list
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }

    }
}
