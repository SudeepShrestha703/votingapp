using Microsoft.AspNetCore.Mvc;
using Voting.Application.Candidates.Commands;
using Voting.Application.Candidates.Queries;
using Voting.Application.Common.Exceptions;

namespace Voting.Api.Controllers
{
    public class CandidatesController : BaseController
    {
        [Produces("application/json")]
        [ProducesResponseType(typeof(ListCandidatesResponse), 200)]
        [HttpGet]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            var query = new ListCandidatesQuery();
            var reponse = await Mediator.Send(query, cancellationToken);
            return Ok(reponse);
        }

        [Produces("application/json")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCandidateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await Mediator.Send(command, cancellationToken);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
