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
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
//using Newtonsoft.Json;
using System.Text.Json;

namespace DevaLokaVillaAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/VillaAPI")]
    // [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaAPIController : ControllerBase
    {
        // private readonly ILogger<VillaAPIController> _logger;
        // private readonly ILogging _logger;


        //public VillaAPIController(ILogger<VillaAPIController> logger)
        //{
        // _logger = logger;
        //}
        //public VillaAPIController(ILogging logger)
        //{
        //    _logger = logger;
        //}

        // private readonly ApplicationDbContext _context;
        protected APIResponse _response;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
        {
            _dbVilla = dbVilla;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        //  [Authorize]
        //[ResponseCache(Duration = 30)]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<APIResponse>>> GetVillas([FromQuery(Name ="filterOccupancy")] int? occupancy,
            [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {

            //_logger.LogInformation("Getting all villas");
            // _logger.Log("Getting all villas","");
            //return Ok(VillaStore.villaList);
            //return Ok(await _context.Villas.ToListAsync());
            try
            {

                IEnumerable<Villa> villaList;

                if(occupancy > 0)
                {
                    villaList = await _dbVilla.GetAllAsync(u => u.Occupancy == occupancy, pageSize:pageSize,pageNumber:pageNumber);
                }
                else
                {
                    villaList = await _dbVilla.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                }

                if(!string.IsNullOrEmpty(search))
                {
                    villaList = villaList.Where(u => u.Name.ToLower().Contains(search));
                }

                Pagination pagination = new (){ PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }

            return Ok(_response);

        }

        //   [Authorize(Roles = "admin")]
        [HttpGet("{id:int}", Name = "GetVilla")]
        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
      //  [ResponseCache(Location = ResponseCacheLocation.None, NoStore=true)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {

                if (id == 0)
                {
                    //  _logger.Log("Get Villa Error with Id" + id,"error");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
                var villa = await _dbVilla.GetAsync(u => u.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                // return Ok(villa);
                _response.Result = _mapper.Map<VillaDTO>(villa);
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

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return  BadRequest(ModelState);
            //}
            try
            {

                if (await _dbVilla.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                //if(createDTO.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}

                //  createDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

                //  VillaStore.villaList.Add(createDTO);

                Villa villa = _mapper.Map<Villa>(createDTO);


                //Villa model = new()
                //{
                //    Amenity = createDTO.Amenity,
                //    Details = createDTO.Details,
                //  //  Id = createDTO.Id,
                //    ImageUrl = createDTO.ImageUrl,
                //    Name = createDTO.Name,
                //    Occupancy = createDTO.Occupancy,
                //    Rate = createDTO.Rate,
                //    Sqft = createDTO.Sqft
                //};

                await _dbVilla.CreateAsync(villa);
                // await _context.SaveChangesAsync();
                //return Ok(villaDTO);

                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;


        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var villa = await _dbVilla.GetAsync(u => u.Id == id);

                if (villa == null)
                {
                    return NotFound();
                }

                await _dbVilla.RemoveAsync(villa);
                //  await _context.SaveChangesAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                //var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
                //villa.Name = updateDTO.Name;
                //villa.Sqft = updateDTO.Sqft;
                //villa.Occupancy = updateDTO.Occupancy;

                Villa villa = _mapper.Map<Villa>(updateDTO);

                //Villa model = new()
                //{
                //    Amenity = updateDTO.Amenity,
                //    Details = updateDTO.Details,
                //    Id = updateDTO.Id,
                //    ImageUrl = updateDTO.ImageUrl,
                //    Name = updateDTO.Name,
                //    Occupancy = updateDTO.Occupancy,
                //    Rate = updateDTO.Rate,
                //    Sqft = updateDTO.Sqft
                //};

                await _dbVilla.UpdateAsync(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {

            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbVilla.GetAsync(u => u.Id == id, tracked: false);


            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);


            //VillaUpdateDTO villaDTO = new()
            // {
            //     Amenity = villa.Amenity,
            //     Details = villa.Details,
            //     Id = villa.Id,
            //     ImageUrl = villa.ImageUrl,
            //     Name = villa.Name,
            //     Occupancy = villa.Occupancy,
            //     Rate = villa.Rate,
            //     Sqft = villa.Sqft
            // };

            if (villa == null)
            {
                return BadRequest();

            }

            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = _mapper.Map<Villa>(villaDTO);

            //Villa model = new Villa()
            //{
            //    Amenity = villaDTO.Amenity,
            //    Details = villaDTO.Details,
            //    Id = villaDTO.Id,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Name = villaDTO.Name,
            //    Occupancy = villaDTO.Occupancy,
            //    Rate = villaDTO.Rate,
            //    Sqft = villaDTO.Sqft
            //};

            await _dbVilla.UpdateAsync(model);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();



        }
    }
}
