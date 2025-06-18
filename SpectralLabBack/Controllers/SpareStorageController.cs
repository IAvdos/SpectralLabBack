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
		public async Task<ActionResult<SpareStorageResponse>> Create([FromBody] List<SpareStorageRequest> newSpares)
		{
			var spares = newSpares.Select( s  => new SpareStorage(Guid.NewGuid(), s.Laboratory, s.AvailableCount, s.SpareId)).ToList();

			var addedSpares = await _storageRepository.CreateSparesStorageAsync(spares);

			var response = addedSpares.Select( x => new SpareStorageResponse(x.Id,  x.Laboratory, x.AvailableCount,x.SpareId) ).ToList();

			return Ok(response);
		}


		[HttpPost("[action]")]
		public async Task<ActionResult<List<AvailableSpare>>> GetAllAsync([FromBody] AvailableSpareStorage filter)
		{
			return Ok(await _storageRepository.GetWithSpare());
		}


		[HttpPut("[action]")]
		public async Task<ActionResult<SpareStorage>> UpdateAsync([FromBody] List<SpareStorageRequest> spares)
		{
			var updatedSpares = spares.Select(s => new SpareStorage(s.Id, s.Laboratory, s.AvailableCount, s.SpareId)).ToList();

			var result = await _storageRepository.UpdateSpares(updatedSpares);

			return Ok(result);
		}


		[HttpDelete("[action]/{id:guid}")]
		public async Task<Guid> Remove(Guid id)
		{
			return await _storageRepository.RemoveSpareStorageAsync(id);
		}
	}
}
