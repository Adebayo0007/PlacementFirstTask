using Application.Abstractions;
using Application.Model.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationProgramController : ControllerBase
    {
        private readonly IApplicationProgramService _applicationProgramService;
        public ApplicationProgramController(IApplicationProgramService applicationProgramService)
        {
            _applicationProgramService = applicationProgramService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateApplicationProgramWindowAsync(CreateApplicationProgramRequest request)
        {
            var response = await _applicationProgramService.CreateApplicationProgramWindowAsync(request);
            return Ok(response);
        }
        [HttpPut("{applicationProgramWindowId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateApplicationProgramWindowAsync(UpdateApplicationProgramRequest request, Guid applicationProgramWindowId)
        {
            var response = await _applicationProgramService.UpdateApplicationProgramWindowAsync(request, applicationProgramWindowId);
            return Ok(response);
        }
    }
}
