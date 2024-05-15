using Application.Abstractions;
using Application.Model.Dtos.Request;
using Application.Model.Dtos.Response;
using Application.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateQuestionAsync(CreateQuestionRequest request, Guid formWindowId)
        {
            var response = await _questionService.CreateQuestionAsync(request, formWindowId);
            return Ok(response);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateQuestionAsync(UpdateQuestionRequest request, Guid questionId)
        {
            var response = await _questionService.UpdateQuestionAsync(request, questionId);
            return Ok(response);
        }
        [HttpGet("{formWindowId}")]
        [ProducesResponseType(typeof(Result<List<QuestionResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<List<object>>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuestionsAsync(Guid formWindowId)
        {
            var response = await _questionService.GetQuestionsByFormWindowIdAsync(formWindowId);
            return Ok(response);
        }
    }
}
