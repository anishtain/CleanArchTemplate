using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Api.Controllers.v1
{
    public class ValuesController : Commons.CleanBaseController
    {
        public ValuesController(IMediator mediator) : base(mediator) { }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            return Ok(new Commons.Models.BaseResponse<object>(true, 200, new { }));
        }
    }
}
