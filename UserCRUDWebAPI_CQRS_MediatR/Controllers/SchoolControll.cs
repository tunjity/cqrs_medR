using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserCRUDWebAPI_CQRS_MediatR.Features.Commands;
using UserCRUDWebAPI_CQRS_MediatR.Features.Queries;
using UserCRUDWebAPI_CQRS_MediatR.Models;

namespace UserCRUDWebAPI_CQRS_MediatR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly IMediator mediator;
        public SchoolController( IMediator _mediator)
        {
            mediator = _mediator;
        }

      
        [HttpGet("GetSchoolByLgaID")]
        public async Task<ResponseDto> GetSchoolByLgaID([FromQuery]List<int> lgaIds) => await this.mediator.Send(new GetSchoolByLgIds(lgaIds));
      
        [HttpPost("Save")]
        public async Task<ResponseDto> Save(SaveSchoolCommand userDto) => await this.mediator.Send(userDto);

      
    }
}
