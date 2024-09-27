using DevaLokaVillaAPI.Data;
using DevaLokaVillaAPI.Logging;
using DevaLokaVillaAPI.Models;
using DevaLokaVillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DevaLokaVillaAPI.Repository.IRepository;
using System.Runtime.InteropServices;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace DevaLokaVillaAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/VillaNumberAPI")]
    // [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNumberAPIv2Controller : ControllerBase
    {

        protected APIResponse _response;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaNumberAPIv2Controller(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            _response = new();
            _dbVilla = dbVilla;
        }



        // [MapToApiVersion("2.0")]
        [HttpGet("GetValue")]
        public IEnumerable<string> get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }


                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id, includeProperties: "Villa");
                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }


                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }


    }
}
