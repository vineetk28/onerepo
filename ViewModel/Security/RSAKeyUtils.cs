using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace MantiScanServices.ViewModel.Security
{
    public class RSAKeyUtils
    {
        public static RSAParameters GetKey()
        {
            var file = "rsakey.json";
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("Check configuration - cannot find auth key file: " + file);
            }

            var keyParams = JsonConvert.DeserializeObject<RSAParametersWithPrivate>(File.ReadAllText(file));
            return keyParams.ToRSAParameters();
        }

        /// <summary>
        /// Util class to allow restoring RSA parameters from JSON as the normal
        /// RSA parameters class won't restore private key info.
        /// </summary>
        private class RSAParametersWithPrivate
        {
            public byte[] D { get; set; }
            public byte[] DP { get; set; }
            public byte[] DQ { get; set; }
            public byte[] Exponent { get; set; }
            public byte[] InverseQ { get; set; }
            public byte[] Modulus { get; set; }
            public byte[] P { get; set; }
            public byte[] Q { get; set; }

            public void SetParameters(RSAParameters p)
            {
                D = p.D;
                DP = p.DP;
                DQ = p.DQ;
                Exponent = p.Exponent;
                InverseQ = p.InverseQ;
                Modulus = p.Modulus;
                P = p.P;
                Q = p.Q;
            }

            public RSAParameters ToRSAParameters()
            {
                return new RSAParameters
                {
                    D = this.D,
                    DP = this.DP,
                    DQ = this.DQ,
                    Exponent = this.Exponent,
                    InverseQ = this.InverseQ,
                    Modulus = this.Modulus,
                    P = this.P,
                    Q = this.Q
                };
            }
        }
    }
}
