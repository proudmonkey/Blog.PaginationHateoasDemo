using Microsoft.AspNetCore.Mvc;
using PaginationDemo.Dtos;
using PaginationDemo.Extensions;
using PaginationDemo.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace PaginationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbiesController : ControllerBase
    {
        private readonly IHobbyService _service;

        public HobbiesController(IHobbyService service)
        {
            _service = service;
        }

        [HttpGet(Name = nameof(GetHobbyListAsync))]
        [ProducesResponseType(typeof(GetHobbyListResponseDto), Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), Status400BadRequest)]
        public async Task<IActionResult> GetHobbyListAsync(
            [FromQuery] UrlQueryParameters urlQueryParameters, 
            CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var hobbies = await _service.GetByPageAsync(
                                    urlQueryParameters.Limit,
                                    urlQueryParameters.Page,
                                    cancellationToken);

            return Ok(GeneratePageLinks(urlQueryParameters,hobbies));
        }

        private GetHobbyListResponseDto GeneratePageLinks(UrlQueryParameters queryParameters, GetHobbyListResponseDto response)
        {

            if (response.CurrentPage > 1)
            {
                var prevRoute = Url.RouteUrl(nameof(GetHobbyListAsync), new { limit = queryParameters.Limit, page = queryParameters.Page - 1 });

                response.AddResourceLink(LinkedResourceType.Prev, prevRoute);

            }

            if (response.CurrentPage < response.TotalPages)
            {
                var nextRoute = Url.RouteUrl(nameof(GetHobbyListAsync), new { limit = queryParameters.Limit, page = queryParameters.Page + 1 });

                response.AddResourceLink(LinkedResourceType.Next, nextRoute);
            }

            return response;
        }
    }

    public record UrlQueryParameters(int Limit = 50, int Page = 1);
}
