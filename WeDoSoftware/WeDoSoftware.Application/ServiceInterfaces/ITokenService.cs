using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Domain.Entities;

namespace WeDoSoftware.Application.ServiceInterfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
