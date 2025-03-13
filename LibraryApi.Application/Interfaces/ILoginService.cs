using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.Application.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginAsync(string username, string password);
        Task<string> GenerateToken(string email);
    }
}
