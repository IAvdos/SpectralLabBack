using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpectralLabBack.DataAccess;

namespace SpectralLabBack.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProposalsController : ControllerBase
	{
		private readonly ProposalsRepository _proposalRepository;
		private readonly ProposalManager _proposalManager;

		public ProposalsController(ProposalsRepository proposalsRepository, ProposalManager proposalManager)
		{
			_proposalRepository = proposalsRepository;
			_proposalManager = proposalManager;
		}

		[HttpGet("[action]")]
		public async Task<ActionResult<List<Proposal>>> GetAll()
		{
			return await _proposalRepository.GetProposals();
		}


		[HttpPost("[action]")]
		public async Task<ActionResult<Guid>> Create([FromBody] NewProposalRequest newProposal)
		{
			var proposal = new Proposal
				(
					Guid.NewGuid(),
					newProposal.Laboratory,
					newProposal.ProposalYearFor,
					newProposal.CreateDate,
					newProposal.IsFinal,
					newProposal.SparesCount.Select(proposal =>
						new ProposalSpareCount
						(
							Guid.NewGuid(),
							proposal.Count,
							proposal.ReceivedCount,
							proposal.SpareId
						)).ToList()
				);

			var result = await _proposalRepository.CreateAsync(proposal);

			return Ok(result);
		}


		[HttpDelete("[action]/{id:guid}")]
		public async Task<ActionResult<Guid>> Delete(Guid id)
		{
			return await _proposalRepository.RemoveAsync(id);
		}


		[HttpPut("[action]")]
		public async Task<ActionResult<Guid>> Update([FromBody] ProposalRequest proposalRequest)
		{
			var updatedProposal = new Proposal
				(
					proposalRequest.Id,
					proposalRequest.Laboratory,
					proposalRequest.ProposalYearFor,
					proposalRequest.CreateDate,
					proposalRequest.IsFinal,
					proposalRequest.SparesCount.Select(s => new ProposalSpareCount(
						s.Id,
						s.Count,
						s.ReceivedCount,
						s.SpareId
						)).ToList()
				);

			//var result = await _proposalRepository.UpdateProposalAsync(updatedProposal);
			var result = await _proposalManager.UpdateAsync(updatedProposal);

			if (result == Guid.Empty)
			{
				return BadRequest("Wrong proposal ID");
			}

			return Ok(result);
		}
	}
}
