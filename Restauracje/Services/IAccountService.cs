using kurs2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2.Services
{
    public interface IAccountService
    {
        void RegisterUser(CreateAccountDto dto);
        string GenerateJwt(LoginDto dto);
    }
}
