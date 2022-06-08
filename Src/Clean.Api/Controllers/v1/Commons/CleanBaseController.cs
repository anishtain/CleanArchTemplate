using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Api.Controllers.v1.Commons
{
    [ApiVersion("1.0")]
    [Controller]
    [Route("api/v{version}/[Controller]")]
    public class CleanBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public CleanBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
