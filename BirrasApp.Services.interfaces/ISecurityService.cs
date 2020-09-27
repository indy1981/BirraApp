using System;
using System.Collections.Generic;
using System.Text;

namespace BirrasApp.Services.Interfaces
{
    public interface ISecurityService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
