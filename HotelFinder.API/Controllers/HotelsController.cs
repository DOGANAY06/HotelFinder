using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelFinder.Business.Abstract;
using HotelFinder.Entities;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        /// <summary>
        /// Get All Hotel Tüm Otelleri Getirme
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async  Task<IActionResult> Get()
        {//asenkron yaptık bunu 
            var hotel = await _hotelService.GetAllHotels();
            return Ok(hotel);

        }
        /// <summary>
        /// Get Hotel By ID Id ile hotel getirme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")] // route tanımladık 
        //api/hotels/gethotelbyId/2
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel!=null)
            {
                return Ok(hotel);
            }

            return NotFound();
        }
        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel = await _hotelService.GetHotelByName(name);
            if (hotel!=null)
            {
                return Ok(hotel); //200+data
            }
            return NotFound();
        }


        /// <summary>
        /// Yeni otel yaratma create hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            var createdHotel = await _hotelService.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel); //201 +data

            
        }
        /// <summary>
        /// Otel güncelleme
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut] //güncelleme 
        [Route("[action]")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            if ( await _hotelService.GetHotelById(hotel.Id)!=null)
            {//id göre kontolre et
                return Ok(_hotelService.UpdateHotel(hotel)); //200+data 
            }

            return NotFound();
        }
        /// <summary>
        /// Otel silme
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete] //güncelleme 
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (await _hotelService.GetHotelById(id) != null)
            {//id göre kontolre et VAR MI 
                await _hotelService.DeleteHotel(id);
                return Ok(); //200+data 
            }

            return NotFound();
        }

    }
}
