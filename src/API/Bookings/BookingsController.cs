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
    [Route("[controller]")]
    [ApiController]
    public class BookingsController : BaseController
    {
        private readonly IUserService _userService;

        public BookingsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetById(int id)
        {
            return await Mediator.Send(new GetBookingQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> Post(Booking booking)
        {
            return await Mediator.Send(new CreateBookingCommand { Booking = booking });
        }
    }
}