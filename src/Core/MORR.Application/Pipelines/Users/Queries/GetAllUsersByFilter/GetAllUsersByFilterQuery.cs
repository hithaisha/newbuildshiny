using MediatR;
using MORR.Application.DTOs.UserDTOs;
using MORR.Domain.Repositories.Query;

namespace MORR.Application.Pipelines.Users.Queries.GetAllUsersByFilter
{
    public record GetAllUsersByFilterQuery : IRequest<List<UserDTO>>
    {
        public string SearchText { get; set; }
    }

    public class GetAllUsersByFilterQueryHandler : IRequestHandler<GetAllUsersByFilterQuery, List<UserDTO>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public GetAllUsersByFilterQueryHandler(IUserQueryRepository userQueryRepository)
        {
            this._userQueryRepository = userQueryRepository;
        }
        public async Task<List<UserDTO>> Handle(GetAllUsersByFilterQuery request, CancellationToken cancellationToken)
        {
            var listOfUsers = await _userQueryRepository.Query(x => x.IsActive == true);

            if(!string.IsNullOrEmpty(request.SearchText))
            {
                listOfUsers = listOfUsers.Where(x => x.FirstName.Contains(request.SearchText) || x.LastName.Contains(request.SearchText));
            }
            var items = listOfUsers.ToList();
            return items.Select(x => new UserDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                LoyaltyRank = GetLoyaltyRankName(x.LoyaltyPoints),

            }).ToList();
            
        }

        private string GetLoyaltyRankName(int loyaltyPoints)
        {
            if (loyaltyPoints >= 1000)
            {
                return "Gold";
            }
            else if (loyaltyPoints >= 500)
            {
                return "Silver";
            }
            else if (loyaltyPoints >= 100)
            {
                return "Bronze";
            }
            else
            {
                return "Basic";
            }
        }
    }
}
