using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace StudentMultiTool.Backend.Services.Logging
{
    public class Hasher
    {
        public string RawSalt { get; }
        public byte[] Salt { get; }
        public int IterationCount { get; } = 100000;
        public int BytesRequested { get; } = 256 / 8;
        public Hasher()
        {
            this.RawSalt = "teammarvel";
            this.Salt = Encoding.ASCII.GetBytes(this.RawSalt);
        }
        public string HashPassword(string password)
        {
            try
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: Salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: this.IterationCount,
                    numBytesRequested: this.BytesRequested
                ));
                return hashed;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return string.Empty;
        }
        public string HashUsername(string username)
        {
            try
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: username,
                    salt: Salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: this.IterationCount,
                    numBytesRequested: this.BytesRequested
                ));
                return hashed;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return string.Empty;
        }

    }
}
