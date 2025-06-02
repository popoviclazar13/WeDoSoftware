using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.Commands.Trainings;
using WeDoSoftware.Application.Exceptions;
using WeDoSoftware.Domain.RepositoryInterfaces;

namespace WeDoSoftware.Application.Handlers.Trainings
{
    public class DeleteTrainingCommandHandler : IRequestHandler<DeleteTrainingCommand>
    {
        private readonly ITrainingSessionRepository _trainingRepository;

        public DeleteTrainingCommandHandler(ITrainingSessionRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }
        public async Task Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdAsync(request.Id);
            if (training == null)
                throw new NotFoundException("User not found.");

            await _trainingRepository.DeleteAsync(training.Id);
        }
    }
}
