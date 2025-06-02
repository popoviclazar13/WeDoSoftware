using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.TrainingDTO;
using WeDoSoftware.Application.Exceptions;
using WeDoSoftware.Application.Queries.Trainings;
using WeDoSoftware.Domain.RepositoryInterfaces;

namespace WeDoSoftware.Application.Handlers.Trainings
{
    public class GetTrainingByIdQueryHandler : IRequestHandler<GetTrainingByIdQuery, TrainingDto>
    {
        private readonly ITrainingSessionRepository _trainingRepository;
        private readonly IMapper _mapper;

        public GetTrainingByIdQueryHandler(ITrainingSessionRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }
        public async Task<TrainingDto> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdAsync(request.Id);
            if (training == null)
                throw new NotFoundException("Training not found.");

            return _mapper.Map<TrainingDto>(training);
        }
    }
}
