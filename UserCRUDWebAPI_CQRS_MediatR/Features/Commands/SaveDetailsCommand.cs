using Azure;
using MediatR;
using UserCRUDWebAPI_CQRS_MediatR.Context;
using UserCRUDWebAPI_CQRS_MediatR.Entity;
using UserCRUDWebAPI_CQRS_MediatR.Events;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Features.Commands
{
    public record SaveUserDetailsCommand(string FirstName, string LastName, string Department, string Email, string Password) : IRequest<ResponseDto>;
    public record SaveSchoolCommand(int LgaId, string Name, string Address) : IRequest<ResponseDto>;

    public class SaveUserDetailsCommandHandler : IRequestHandler<SaveUserDetailsCommand, ResponseDto>
    {
        private readonly demoDBContext demoDBContext;
        private readonly IMediator mediator;
        public SaveUserDetailsCommandHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }

        public async Task<ResponseDto> Handle(SaveUserDetailsCommand request, CancellationToken cancellationToken)
        {

            ResponseDto response;
            try
            {
                if (request is not null)
                {
                    var userID = Guid.NewGuid();
                    await demoDBContext.Users.AddAsync(new Users
                    {
                       ID = userID,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        Department = request.Department,
                        Password = request.Password,
                    });
                    await demoDBContext.SaveChangesAsync();
                    response = new ResponseDto(userID, "Saved Successfully!",true);
                    mediator.Publish(new ResponseEvent(response));
                    return response;
                }

                response = new ResponseDto(default, "Request is not found!",false);
                mediator.Publish(new ResponseEvent(response));
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
    public class SaveSchoolCommandHandler : IRequestHandler<SaveSchoolCommand, ResponseDto>
    {
        private readonly demoDBContext demoDBContext;
        private readonly IMediator mediator;
        public SaveSchoolCommandHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }

        public async Task<ResponseDto> Handle(SaveSchoolCommand request, CancellationToken cancellationToken)
        {

            ResponseDto response;
            try
            {
                if (request is not null)
                {
                    var userID = Guid.NewGuid();
                    await demoDBContext.Schools.AddAsync(new School
                    {
                        Name = request.Name,
                        LgaId = request.LgaId,
                        Address = request.Address
                    });
                    await demoDBContext.SaveChangesAsync();
                    response = new ResponseDto(userID, "Saved Successfully!",true);
                    mediator.Publish(new ResponseEvent(response));
                    return response;
                }

                response = new ResponseDto(default, "Request is not found!",false);
                mediator.Publish(new ResponseEvent(response));
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
