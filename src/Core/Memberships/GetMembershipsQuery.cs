using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Memberships
{
    public class GetMembershipsQuery : IRequest<List<Membership>>
    {
    }

    public class GetMembershipsQueryHandler : IRequestHandler<GetMembershipsQuery, List<Membership>>
    {
        private readonly IMembershipRepository _membershipRepository;

        public GetMembershipsQueryHandler(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<List<Membership>> Handle(GetMembershipsQuery request, CancellationToken cancellationToken)
        {
            return await _membershipRepository.FindAll();
        }
    }
}
