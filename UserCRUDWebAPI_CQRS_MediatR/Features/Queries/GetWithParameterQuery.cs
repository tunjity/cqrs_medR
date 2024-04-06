using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserCRUDWebAPI_CQRS_MediatR.Context;
using UserCRUDWebAPI_CQRS_MediatR.Events;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Features.Queries
{
    public record GetUserDetailsByUserIDQuery(Guid userID) : IRequest<ResponseDto>;
    public record GetUserDetailsByUserPasswordQuery(string password) : IRequest<ResponseDto>;
    public record GetSchoolByLgIds(List<int> lgaIds) : IRequest<ResponseDto>;
    public record GetSchoolById(Guid ID) : IRequest<ResponseDto>;


    public class GetWithParameterQueryHandler : IRequestHandler<GetUserDetailsByUserIDQuery, ResponseDto>
    {
        private readonly demoDBContext demoDBContext;
        private readonly IMediator mediator;

        public GetWithParameterQueryHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }


        public async Task<ResponseDto> Handle(GetUserDetailsByUserIDQuery request, CancellationToken cancellationToken)
        {
            ResponseDto response = null;
            var _userDetails = await demoDBContext.Users.FirstOrDefaultAsync(o=>o.ID == request.userID);
            if (_userDetails is not null)
            {  var res= new UserDto
                {
                    UserID = _userDetails.ID,
                    FirstName = _userDetails.FirstName,
                    LastName = _userDetails.LastName,
                    Department = _userDetails.Department,
                    Email = _userDetails.Email,
                    Password = _userDetails.Password
                };
                response = new ResponseDto(res,"User Found",true);
            }
            else
            {
                response = new ResponseDto(request.userID, "User details not found!", false);
                await mediator.Publish(new ErrorEvent(response));
            }
            return response;
        }
    }
    public class GetUserDetailsByUserPasswordQueryHandler : IRequestHandler<GetUserDetailsByUserPasswordQuery, ResponseDto>
    {
        private readonly demoDBContext demoDBContext;
        private readonly IMediator mediator;

        public GetUserDetailsByUserPasswordQueryHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }
        public async Task<ResponseDto> Handle(GetUserDetailsByUserPasswordQuery request, CancellationToken cancellationToken)
        {
            ResponseDto response = null;
            var _userDetails = await demoDBContext.Users.FirstOrDefaultAsync(o=>o.Password ==request.password);
            if (_userDetails is not null)
            {
                var res= new UserDto
                {
                    UserID = _userDetails.ID,
                    FirstName = _userDetails.FirstName,
                    LastName = _userDetails.LastName,
                    Department = _userDetails.Department,
                    Email = _userDetails.Email,
                    Password = _userDetails.Password
                };
                response = new ResponseDto(res,"User Found",true);
            }
            else
            {
                response = new ResponseDto(request.password, "User details not found!", false);
                await mediator.Publish(new ErrorEvent(response));
            }

            return response;
        }
    }
    public class GetSchoolByIdQueryHandler : IRequestHandler<GetSchoolById, ResponseDto>
    {
        private readonly demoDBContext demoDBContext;
        private readonly IMediator mediator;

        public GetSchoolByIdQueryHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }


        public async Task<ResponseDto> Handle(GetSchoolById request, CancellationToken cancellationToken)
        {
            ResponseDto response = null;
            var _res = await demoDBContext.Schools.FirstOrDefaultAsync(o=>o.ID == request.ID);
            if (_res is not null)
            {  var res= new SchoolDto
                {
                    ID = _res.ID,
                    LgaId = _res.LgaId,
                    Name = _res.Name
                };
                response = new ResponseDto(res,"User Found",true);
            }
            else
            {
                response = new ResponseDto(request.ID, "User details not found!", false);
                await mediator.Publish(new ErrorEvent(response));
            }
            return response;
        }
    }
    // public class GetSchoolByLGAQueryHandler : IRequestHandler<GetSchoolByLgIds, ResponseDto>
    // {
    //     private readonly demoDBContext demoDBContext;
    //     private readonly IMediator mediator;

    //     public GetSchoolByLGAQueryHandler(demoDBContext _demoDBContext, IMediator _mediator) { demoDBContext = _demoDBContext; mediator = _mediator; }
    //     public async Task<ResponseDto> Handle(GetSchoolByLgIds request, CancellationToken cancellationToken)
    //     {
    //         ResponseDto response = null;
    //         var _userDetails = await demoDBContext.Users.FirstOrDefaultAsync(o=>o.Password ==request.password);
    //         if (_userDetails is not null)
    //         {
    //             var res= new UserDto
    //             {
    //                 UserID = _userDetails.ID,
    //                 FirstName = _userDetails.FirstName,
    //                 LastName = _userDetails.LastName,
    //                 Department = _userDetails.Department,
    //                 Email = _userDetails.Email,
    //                 Password = _userDetails.Password
    //             };
    //             response = new ResponseDto(res,"User Found",true);
    //         }
    //         else
    //         {
    //             response = new ResponseDto(request.password, "User details not found!", false);
    //             await mediator.Publish(new ErrorEvent(response));
    //         }

    //         return response;
    //     }
    // }

}
