using AutoMapper;
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
    public class UpdateTrainingCommandHandler : IRequestHandler<UpdateTrainingCommand>
    {
        private readonly ITrainingSessionRepository _trainingRepository;
        private readonly IMapper _mapper;

        public UpdateTrainingCommandHandler(ITrainingSessionRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }
        public async Task Handle(UpdateTrainingCommand request, CancellationToken cancellationToken)
        {
            var existingTraining = await _trainingRepository.GetByIdAsync(request.Id);
            if (existingTraining == null)
                throw new NotFoundException("User not found.");

            _mapper.Map(request.Dto, existingTraining);

            await _trainingRepository.UpdateAsync(existingTraining);
        }
    }
}
