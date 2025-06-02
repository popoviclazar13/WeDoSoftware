﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDoSoftware.Application.Commands.User
{
    public record DeleteUserCommand(int Id) : IRequest;
}
