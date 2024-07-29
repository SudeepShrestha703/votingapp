using Microsoft.AspNetCore.Mvc;
using Voting.Application.CastVote.Commands;
using Voting.Application.Common.Exceptions;

namespace Voting.Api.Controllers
{
    public class CastVoteController : BaseController
    {
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [HttpPost("votes")]
        public async Task<IActionResult> CastVote([FromBody] CastVoteCommand command, CancellationToken cancellationToken)
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
