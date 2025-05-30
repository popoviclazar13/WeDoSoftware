using Microsoft.AspNetCore.Mvc;
using WeDoSoftware.Application.DTOs.TrainingDTO;
using WeDoSoftware.Application.ServiceInterfaces;

namespace WeDoSoftware.WebApi.Controllers
{
    [ApiController]
    [Route("api/trainings")]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        // GET: api/training
        [HttpGet]
        public async Task<IEnumerable<TrainingDto>> GetAll()
        {
            return await _trainingService.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingDto>> GetById(int id)
        {
            var training = await _trainingService.GetByIdAsync(id);
            if (training == null)
                return NotFound();

            return Ok(training);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainingDto dto)
        {
            var trainingId = await _trainingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = trainingId }, dto);
        }
        // PUT: api/training/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTrainingDto dto)
        {
            await _trainingService.UpdateAsync(id, dto);
            return NoContent();
        }
        // DELETE: api/training/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _trainingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
