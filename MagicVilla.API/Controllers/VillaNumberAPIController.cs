using AutoMapper;
using MagicVilla.API.Models;
using MagicVilla.API.Models.Dto;
using MagicVilla.API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla.API.Controllers
{
    [Route("api/villanumberAPI")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        protected Respone _respone;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;

        private readonly IMapper _mapper;

        public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            _respone = new Respone();
            _dbVilla = dbVilla;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Respone>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> VillaNumberList = await _dbVillaNumber.GetAllAsync();
                _respone.Result = _mapper.Map<IEnumerable<VillaNumberDTO>>(VillaNumberList);
                _respone.StatusCode = HttpStatusCode.OK;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _respone;
        }
        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Respone>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _respone.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_respone);
                }
                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
                if (villaNumber == null)
                {
                    _respone.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_respone);
                }
                _respone.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _respone.StatusCode = HttpStatusCode.OK;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _respone;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Respone>> CreateVillaNumber([FromBody] VillaNumberCreateDTO villaNumberCreateDTO)
        {
            try
            {
                if (villaNumberCreateDTO == null)
                {
                    return BadRequest(villaNumberCreateDTO);
                }

                if (await _dbVilla.GetAsync(u => u.Id == villaNumberCreateDTO.VillaId) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa Id is Invalid!");
                    return BadRequest(ModelState);
                }

                var villaNumber = _mapper.Map<VillaNumber>(villaNumberCreateDTO);

                await _dbVillaNumber.CreateAsync(villaNumber);
                await _dbVillaNumber.SaveAsync();

                _respone.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _respone.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _respone;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<Respone>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await _dbVillaNumber.RemoveAsync(villa);
                await _dbVillaNumber.SaveAsync();

                _respone.StatusCode = HttpStatusCode.NoContent;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _respone;

        }
        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Respone>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO villaNumberUpdateDTO)
        {
            try
            {
                if (villaNumberUpdateDTO == null || id != villaNumberUpdateDTO.VillaNo)
                {
                    return BadRequest();
                }
                if (await _dbVilla.GetAsync(u => u.Id == villaNumberUpdateDTO.VillaId) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa Id is Invalid!");
                    return BadRequest(ModelState);
                }
                VillaNumber model = _mapper.Map<VillaNumber>(villaNumberUpdateDTO);

                await _dbVillaNumber.UpdateAsync(model);
                await _dbVillaNumber.SaveAsync();

                _respone.StatusCode = HttpStatusCode.NoContent;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _respone;

        }

    }
}
