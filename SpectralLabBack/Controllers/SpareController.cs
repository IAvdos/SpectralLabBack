using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpectralLabBack.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class SpareController : ControllerBase
	{
		private readonly SparesRepository _repository;
		private readonly RequestValidator _validator;

		public SpareController(SparesRepository repository, RequestValidator validator)
		{
			_repository = repository;
			_validator = validator;
		}

		[HttpGet("[action]")]
		public async Task<ActionResult<List<Spare>>> Get()
		{
			return await _repository.GetAll();
		}

		[HttpPost("[action]")]
		public async Task<ActionResult<Guid>> Add(NewSpareRequest request)
		{
			var spare = new Spare(Guid.NewGuid(), request.Name, request.Equipment, request.CatalogName, request.OzmId);

			var validationResult = _validator.ValidateNewSpare(spare);

			if(validationResult.Item1 == false)
			{
				return BadRequest(validationResult.Item2);
			}

			var responce = await _repository.Create(spare);

			return Ok(responce);
		}
	}
}
