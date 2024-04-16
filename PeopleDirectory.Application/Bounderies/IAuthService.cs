using PeopleDirectoryAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDirectory.Application.Bounderies
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(LoginRequestDto model);
    }
}
