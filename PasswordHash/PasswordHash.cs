// ************************************************************************************************************************************************************
// <copyright file="PasswordHash.cs" company="Thomas Weller Software-Entwicklung">
//  Copyright (c) 2014, Thomas Weller Software-Entwicklung. All rights reserved.
// </copyright>
// <authors>
//   <author>Thomas Weller</author>
// </authors>
// <license name="Creative Commons Attribution-Share Alike 3.0 Unported License" url="http://creativecommons.org/licenses/by-sa/3.0/" />
// <Description>
//    This is a PCL compatible C# implementation of a secure password hashing algorithm as described in the CodeProject article 'Salted Password Hashing - 
//    Doing it Right'.    
// </Description>
// <Credits>
//    CodeProject article 'Salted Password Hashing - Doing it Right': http://www.codeproject.com/Articles/704865/Salted-Password-Hashing-Doing-it-Right   
//    Associated sample code: https://crackstation.net/hashing-security.htm#aspsourcecode
// </Credits>
// ************************************************************************************************************************************************************

using System;
using System.Text;
using Org.BouncyCastle.Security;

namespace PasswordHash
{
    public static class PasswordHash
    {
        #region // Constants

        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int Pbkdf2Index = 2;

        private const char Delimiter = ':';

        #endregion // Constants

        #region Fields

        private static readonly object LockObject = new object();

        private static int _saltByteSize;
        private static int _hashByteSize;
        private static int _pbkdf2Iterations;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Length (in bytes) of the generated hash.
        /// </summary>
        /// <remarks>
        /// Can be between 32 and 128, default is 64.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown when trying to set a value less than 32 or greater than 128.
        /// </exception>
        public static int HashByteSize
        {
            get { return _hashByteSize; }
            set
            {
                if (value < 32 || value > 128)
                {
                    throw new ArgumentException("Value must be between 12 and 36.");
                }
                _hashByteSize = value;
            }
        }

        /// <summary>
        /// Number of iterations for the hashing algorithm.
        /// </summary>
        /// <remarks>
        /// Can be between 500 and 2000, default is 1000.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown when trying to set a value less than 500 or greater than 2000.
        /// </exception>
        public static int Pbkdf2Iterations
        {
            get { return _pbkdf2Iterations; }
            set
            {
                if (value < 500 || value > 2000)
                {
                    throw new ArgumentException("Value must be between 500 and 2000.");
                }
                _pbkdf2Iterations = value;
            }
        }

        /// <summary>
        /// Length (in bytes) of the password salt.
        /// </summary>
        /// <remarks>
        /// Can be between 32 and 128, default is 64.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown when trying to set a value less than 32 or greater than 128.
        /// </exception>
        public static int SaltByteSize
        {
            get { return _saltByteSize; }
            set
            {
                if (value < 32 || value > 128)
                {
                    throw new ArgumentException("Value must be between 12 and 36.");
                }
                _saltByteSize = value;
            }
        }

        #endregion

        #region Construction

        static PasswordHash()
        {
            SetDefaults();
        }

        #endregion // Construction

        #region Operations

        /// <summary>
        /// Sets defaults: 64 for <see cref="HashByteSize"/> and <see cref="SaltByteSize"/>,
        /// 1000 for <see cref="Pbkdf2Iterations"/>.
        /// </summary>
        public static void SetDefaults()
        {
            lock (LockObject)
            {
                _hashByteSize = 64;
                _saltByteSize = 64;
                _pbkdf2Iterations = 1000;
            }
        }

        /// <summary>
        /// Creates a salted Pbkdf2 hash of the password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hash of the password.</returns>
        public static string CreateHash(string password)
        {
            lock (LockObject)
            {
                var salt = CreateRandomSalt();

                // Hash the password and encode the parameters
                byte[] hash = new Pbkdf2().GenerateDerivedKey(HashByteSize, Encoding.UTF8.GetBytes(password), salt, Pbkdf2Iterations);

                return Pbkdf2Iterations.ToString() +
                       Delimiter +
                       Convert.ToBase64String(salt) +
                       Delimiter +
                       Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="correctHash">A hash of the correct password.</param>
        /// <returns>True if the password is correct. False otherwise.</returns>
        public static bool ValidatePassword(string password, string correctHash)
        {
            lock (LockObject)
            {
                // Extract the parameters from the hash
                string[] hashParts = correctHash.Split(Delimiter);
                int iterations = Int32.Parse(hashParts[IterationIndex]);
                byte[] salt = Convert.FromBase64String(hashParts[SaltIndex]);
                byte[] hash = Convert.FromBase64String(hashParts[Pbkdf2Index]);

                byte[] testHash = new Pbkdf2().GenerateDerivedKey(hash.Length, Encoding.UTF8.GetBytes(password), salt, iterations);

                return SlowEquals(hash, testHash);
            }
        }

        #endregion // Operations

        #region Implementation

        internal static byte[] CreateRandomSalt()
        {
            Random random = SecureRandom.GetInstance("SHA256PRNG", true);

            var salt = new byte[SaltByteSize];
            random.NextBytes(salt);

            return salt;
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. false otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        #endregion // Implementation
    }
}
