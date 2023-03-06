using API.Model;
using DTO.ViewModel.Hotel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Unit;
using Service.Models;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/Hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IServiceUnit _service;

        public HotelController(IServiceUnit service)
        {
            _service = service;
        }

        [HttpGet("GetHotelData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceResult<HotelInputDto>>> GetHotelData([FromQuery] HotelDataRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (request.HotelId < 1)
                    return BadRequest(ServiceResults.Errors.Required<object>("HotelId", null));

                DateTime arrivalDate;
                if (!string.IsNullOrEmpty(request.TargetDate))
                {
                    arrivalDate = DateTime.Parse(request.TargetDate);
                    if (arrivalDate <= DateTime.MinValue)
                        return BadRequest(ServiceResults.Errors.Required<object>("arrivalDate", null));
                }
                else
                    return BadRequest(ServiceResults.Errors.Required<object>("arrivalDate", null));

                var result = await _service.Hotel.GetHotelData(request.HotelId, arrivalDate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResults.Errors.UnhandledError<object>(ex.Message.ToString(), null));
            }
        }
    }
}
