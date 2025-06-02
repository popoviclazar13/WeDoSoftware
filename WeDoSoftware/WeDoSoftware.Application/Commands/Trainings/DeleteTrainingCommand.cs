using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDoSoftware.Application.Commands.Trainings
{
    public record DeleteTrainingCommand(int Id) : IRequest;
}
