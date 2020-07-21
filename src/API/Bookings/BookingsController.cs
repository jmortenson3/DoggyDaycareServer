using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Common;
using API.Users;
using Core.Bookings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Bookings
{
    [ApiController]
    public class BookingsController : BaseController
    {
        private readonly IUserService _userService;

        public BookingsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/bookings/{id}")]
        public async Task<ActionResult<Booking>> GetById(int id)
        {
            return await Mediator.Send(new GetBookingQuery { Id = id });
        }

        [HttpPost]
        [Route("/bookings")]
        public async Task<ActionResult<Booking>> Post(CreateBookingCommand body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            body.CreatedBy = user.Id;
            body.CreatedUtc = DateTime.Now;
            return await Mediator.Send(body);
        }

        [HttpPut]
        [Route("/bookings/{id}")]
        public async Task<ActionResult<Booking>> Put(int id, UpdateBookingCommand body)
        {
            var user = await _userService.GetCurrentUser(HttpContext.User);
            body.Booking.Id = id;
            body.Booking.LastModifiedBy = user.Id;
            body.Booking.LastModifiedUtc = DateTime.UtcNow;
            return await Mediator.Send(body);
        }
    }
}