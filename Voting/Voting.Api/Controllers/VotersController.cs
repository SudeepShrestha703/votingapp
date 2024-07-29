using Microsoft.AspNetCore.Mvc;
using Voting.Application.Common.Exceptions;
using Voting.Application.Voters.Commands;
using Voting.Application.Voters.Queries;

namespace Voting.Api.Controllers
{
    public class VotersController : BaseController
    {
        [Produces("application/json")]
        [ProducesResponseType(typeof(ListVotersResponse), 200)]
        [HttpGet]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            var query = new ListVotersQuery();
            var reponse = await Mediator.Send(query, cancellationToken);
            return Ok(reponse);
        }

        [Produces("application/json")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVoterCommand command, CancellationToken cancellationToken)
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
