﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using UserCRUDWebAPI_CQRS_MediatR.Context;
using UserCRUDWebAPI_CQRS_MediatR.Events;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Features.Commands
{
    public record UpdateDetailsCommand(Guid UserID, string FirstName, string LastName, string Department, string Email, string Password) : IRequest<ResponseDto>;

    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateDetailsCommand, ResponseDto>
    {
        private readonly demoDBContext demoDBContext;
        private readonly IMediator mediator;
        public UpdateUserDetailsCommandHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }

        public async Task<ResponseDto> Handle(UpdateDetailsCommand request, CancellationToken cancellationToken)
        {
            ResponseDto response;
            try
            {
                if (request is not null)
                {
                    var userDetails = await demoDBContext.Users.FirstOrDefaultAsync(x => x.ID == request.UserID);
                    if (userDetails != null)
                    {
                        userDetails.FirstName = request.FirstName;
                        userDetails.LastName = request.LastName;
                        userDetails.Email = request.Email;
                        userDetails.Department = request.Department;
                        userDetails.Password = request.Password;

                        await demoDBContext.SaveChangesAsync();
                        response = new ResponseDto(userDetails.ID, "Updated Successfully!",true);
                        mediator.Publish(new ResponseEvent(response));
                        return response;
                    }
                    else
                    {
                        response = new ResponseDto(request.UserID, "User ID not found in the Database!",false);
                        mediator.Publish(new ErrorEvent(response));
                        return response;
                    }
                }
                response = new ResponseDto(request.UserID, "User ID not found in the Database!",false);
                mediator.Publish(new ErrorEvent(response));
                return response;
            }
            catch
            {
                response = new ResponseDto(default, "Failed!",false);
                mediator.Publish(new ErrorEvent(response));
                return response;
            }
        }
    }
}
