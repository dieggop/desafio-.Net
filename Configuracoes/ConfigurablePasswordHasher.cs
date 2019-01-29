using System;
using desafio_.Net.Models;
using Microsoft.AspNetCore.Identity;

namespace desafio_.Net.Configuracoes
{
    public class ConfigurablePasswordHasher : IPasswordHasher<Usuario>
    {
        private readonly int iterationCount;
        public ConfigurablePasswordHasher(int iterationCount = 10000)
        {
            if (iterationCount < 1)
            {
                throw new ArgumentOutOfRangeException("iterationCount", "Password has iteration count cannot be less than 1");
            }

            this.iterationCount = iterationCount;
        }

        public string HashPassword(Usuario user, string password)
        {
        return Crypto.HashPassword(password, iterationCount);
        }

        public PasswordVerificationResult VerifyHashedPassword(Usuario user, string hashedPassword, string providedPassword)
        {
             if (Crypto.VerifyHashedPassword(hashedPassword, providedPassword, iterationCount)) {
                return PasswordVerificationResult.Success;
                }
            return PasswordVerificationResult.Failed;
        }


    }

 
}