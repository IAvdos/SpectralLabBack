using Microsoft.AspNetCore.Mvc;

namespace SpectralLabBack.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SpareStorageController : ControllerBase
	{
		private readonly SparesStorageRepository _storageRepository;

		public SpareStorageController(SparesStorageRepository storageRepository)
		{
			_storageRepository = storageRepository;
		}

		[HttpPost("[action]")]
		public async Task<ActionResult<AddSpareStorageResponse>> Create([FromBody] List<SpareStorageRequest> newSpares)
		{
			var spares = newSpares.Select( s  => new SpareStorage(Guid.NewGuid(), s.Laboratory, s.AvailableCount, s.SpareId)).ToList();

			var alreadyConteins = await _storageRepository.CreateSparesStorageAsync(spares);

			var response = alreadyConteins.Select( x => new AddSpareStorageResponse(x.SpareId, x.Laboratory) ).ToList();

			return Ok(response);
		}

		[HttpGet("[action]")]
		public async Task<ActionResult<SpareStorage>> GetAllAsync()
		{
			return Ok(await _storageRepository.GetAllAsync());
		}

		[HttpDelete("[action]/{id:guid}")]
		public async Task<Guid> Remove(Guid id)
		{
			return await _storageRepository.RemoveSpareStorageAsync(id);
		}
	}
}
