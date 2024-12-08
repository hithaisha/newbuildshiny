using MediatR;
using MORR.Application.Common.Extentions;
using MORR.Application.Common.Interfaces;
using MORR.Application.DTOs.Common;
using MORR.Application.DTOs.UserDTOs;
using MORR.Domain.Entities;
using MORR.Domain.Repositories.Command;
using MORR.Domain.Repositories.Query;

namespace MORR.Application.Pipelines.Users.Commands.SaveCustomer
{
    public record SaveCustomerCommand : IRequest<ResultDto> 
    {
        public UserDTO  UserDetails { get; set; }
    }

    public class SaveCustomerCommandHandler : IRequestHandler<SaveCustomerCommand, ResultDto>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        public SaveCustomerCommandHandler(IUserQueryRepository userQueryRepository, IUserCommandRepository userCommandRepository)
        {
            this._userQueryRepository = userQueryRepository;
            this._userCommandRepository = userCommandRepository;
        }
        public async Task<ResultDto> Handle(SaveCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userQueryRepository.GetById(request.UserDetails.Id, cancellationToken);

                if(user is null)
                {
                    user = request.UserDetails.ToEntity();

                    user.UserRoles.Add(new UserRole()
                    {
                        UserId = user.Id,
                        RoleId = 3,
                        IsActive = true,
                    });

                    await _userCommandRepository.AddAsync(user, cancellationToken);

                    return ResultDto.Success("Resgistration Successfull");

                }
                else
                {
                    user = request.UserDetails.ToEntity(user);

                    return ResultDto.Success("Update Subccessfull");
                }

               

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    
    }
}
