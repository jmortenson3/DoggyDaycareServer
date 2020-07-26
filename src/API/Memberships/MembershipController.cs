using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Common;
using Core.Memberships;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Memberships
{
    [ApiController]
    [Authorize]
    public class MembershipController : BaseController
    {
        private readonly IMediator _mediator;

        public MembershipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/memberships")]
        public async Task<ActionResult<List<Membership>>> Get()
        {
            return await _mediator.Send(new GetMembershipsQuery());
        }
    }
}
