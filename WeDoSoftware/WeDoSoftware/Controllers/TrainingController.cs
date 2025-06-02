using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeDoSoftware.Application.Commands.Trainings;
using WeDoSoftware.Application.DTOs.TrainingDTO;
using WeDoSoftware.Application.Queries.Trainings;
using WeDoSoftware.Application.ServiceInterfaces;

namespace WeDoSoftware.WebApi.Controllers
{
    [ApiController]
    [Route("api/trainings")]
    public class TrainingController : ControllerBase
    {
        /*private readonly ITrainingService _trainingService;

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
        }*/
        private readonly IMediator _mediator;

        public TrainingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrainings()
        {
            var query = new GetAllTrainingsQuery();
            var trainings = await _mediator.Send(query);
            return Ok(trainings);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingById(int id)
        {
            var query = new GetTrainingByIdQuery(id);
            var training = await _mediator.Send(query);
            return Ok(training);
        }
        [HttpGet("by-user-and-month")]
        public async Task<IActionResult> GetTrainingsByUserAndMonth([FromQuery] int userId, [FromQuery] int year, [FromQuery] int month)
        {
            var query = new GetTrainingsByUserAndMonthQuery(userId, year, month);
            var trainings = await _mediator.Send(query);
            return Ok(trainings);
        }
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetAllTrainingsByUserId(int userId)
        {
            var query = new GetAllTrainingsByUserIdQuery(userId);
            var trainings = await _mediator.Send(query);
            return Ok(trainings);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTraining([FromBody] CreateTrainingDto dto)
        {
            var command = new CreateTrainingCommand(dto);
            await _mediator.Send(command);
            return Ok("Training successfully created.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTraining(int id, [FromBody] UpdateTrainingDto dto)
        {
            var command = new UpdateTrainingCommand(id, dto);
            await _mediator.Send(command);
            return Ok("Training successfully updated.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            var command = new DeleteTrainingCommand(id);
            await _mediator.Send(command);
            return Ok("Training successfully deleted.");
        }
    }
}
