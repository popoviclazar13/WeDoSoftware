using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.TrainingDTO;
using WeDoSoftware.Domain.Entities;

namespace WeDoSoftware.Application.Mappings
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<Training, TrainingDto>().ReverseMap();
            CreateMap<CreateTrainingDto, Training>();
            CreateMap<UpdateTrainingDto, Training>();
        }
    }
}
