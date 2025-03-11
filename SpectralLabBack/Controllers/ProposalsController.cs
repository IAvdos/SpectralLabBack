using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpectralLabBack.DataAccess;

namespace SpectralLabBack.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProposalsController : ControllerBase
	{
		private readonly ProposalsRepository _dbRepository;

		public ProposalsController(ProposalsRepository proposalsRepository)
		{
			_dbRepository = proposalsRepository;
		}

		[HttpGet("[action]")]
		public async Task<ActionResult<List<Proposal>>> GetAll()
		{
			return await _dbRepository.GetProposals();
		}


		[HttpPost("[action]")]
		public async Task<ActionResult<Guid>> Create([FromBody] NewProposalRequest newProposal)
		{
			var result = await _dbRepository.CreateAsync(newProposal);
			return Ok(result);
		}

		[HttpDelete("[action]")]
		public async Task<ActionResult<Guid>> Delete(Guid id)
		{
			return await _dbRepository.RemoveAsync(id);
		}
	}
}
