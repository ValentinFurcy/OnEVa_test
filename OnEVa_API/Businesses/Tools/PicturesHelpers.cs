using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Businesses.Tools
{
    public class PicturesHelpers
    {
        public async Task<bool> ComparePictureByHash(byte[] picture1Data, byte[] picture2Data)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash1 = sha256.ComputeHash(picture1Data);
                var hash2 = sha256.ComputeHash(picture2Data);

                return hash1.SequenceEqual(hash2);
            }
        }
    }
}
