using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HotelFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Eğer Bu attribute'yi eklersek otomatik olarak validation konrollerini yapıyor
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
        
           
            _hotelService = hotelService;
        }
        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var hotel= _hotelService.GetAllHotels();
            return Ok(hotel);// status: 200 + data
        }
        /// <summary>
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var hotel = _hotelService.GetHotelsById(id);

            if (hotel!=null)
            {
                return Ok(hotel);// status:200 + data

            }
            return NotFound();// status:404
        }
        /// <summary>
        /// Create A Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]        
        public IActionResult Post([FromBody] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                var createdHotel=_hotelService.CreateHotel(hotel);
                return CreatedAtAction("Get", new { createdHotel.Id }, createdHotel);//201 + Data
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Update A Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] Hotel hotel)
        {
            if (_hotelService.GetHotelsById(hotel.Id)!=null)
            {
                return Ok(_hotelService.UpdateHotel(hotel)); // status:200 + data
            }
            return NotFound();
        }
        /// <summary>
        /// Delete A Hotel
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_hotelService.GetHotelsById(id)!=null)
            {
                _hotelService.DeleteHotel(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
