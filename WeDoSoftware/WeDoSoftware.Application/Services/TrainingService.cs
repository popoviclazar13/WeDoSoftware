using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.TrainingDTO;
using WeDoSoftware.Application.Exceptions;
using WeDoSoftware.Application.ServiceInterfaces;
using WeDoSoftware.Domain.Entities;
using WeDoSoftware.Domain.RepositoryInterfaces;

namespace WeDoSoftware.Infrastructure.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingSessionRepository _trainingRepository;
        private readonly IMapper _mapper;

        public TrainingService(ITrainingSessionRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(CreateTrainingDto dto)
        {
            var training = _mapper.Map<Training>(dto);
            await _trainingRepository.AddAsync(training);
            return training.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var training = await _trainingRepository.GetByIdAsync(id);
            if (training == null)
                throw new NotFoundException($"Training with id {id} not found.");

            await _trainingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TrainingDto>> GetAllAsync()
        {
            var trainings = await _trainingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TrainingDto>>(trainings);
        }

        public async Task<IEnumerable<TrainingDto>> GetAllByUserIdAsync(int userId)
        {
            var trainings = await _trainingRepository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TrainingDto>>(trainings);
        }

        public async Task<TrainingDto> GetByIdAsync(int id)
        {
            var training = await _trainingRepository.GetByIdAsync(id);
            if (training == null)
                throw new NotFoundException($"Training with ID {id} not found.");

            return _mapper.Map<TrainingDto>(training);
        }

        public async Task<IEnumerable<TrainingDto>> GetByUserAndMonthAsync(int userId, int year, int month)
        {
            var trainings = await _trainingRepository.GetByUserAndMonthAsync(userId, year, month);
            return _mapper.Map<IEnumerable<TrainingDto>>(trainings);
        }

        public async Task UpdateAsync(int id, UpdateTrainingDto dto)
        {
            var training = await _trainingRepository.GetByIdAsync(id);
            if (training == null)
                throw new NotFoundException($"Training with id {id} not found.");

            _mapper.Map(dto, training);
            await _trainingRepository.UpdateAsync(training);
        }

    }
}
