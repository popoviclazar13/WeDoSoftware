using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.Commands.Trainings;
using WeDoSoftware.Domain.Entities;
using WeDoSoftware.Domain.RepositoryInterfaces;

namespace WeDoSoftware.Application.Handlers.Trainings
{
    public class CreateTrainingCommandHandler : IRequestHandler<CreateTrainingCommand, int>
    {
        private readonly ITrainingSessionRepository _trainingRepository;
        private readonly IMapper _mapper;

        public CreateTrainingCommandHandler(ITrainingSessionRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTrainingCommand request, CancellationToken cancellationToken)
        {
            var trainingEntity = _mapper.Map<Training>(request.Dto);
            await _trainingRepository.AddAsync(trainingEntity);
            return trainingEntity.Id; 
        }
    }
}
