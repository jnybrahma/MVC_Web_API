using DevaLokaVilla_WebApp.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevaLokaVilla_WebApp.Models.VM
{
    public class VillaNumberCreateVM
    {
        public  VillaNumberCreateVM()
        {
            VillaNumber = new VillaNumberCreateDTO();
        }

        public VillaNumberCreateDTO VillaNumber { get; set; }
        // create dropdown for villa list
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }

    }
}
