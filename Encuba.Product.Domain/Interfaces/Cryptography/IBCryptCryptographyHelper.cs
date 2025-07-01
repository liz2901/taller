namespace Encuba.Product.Domain.Interfaces.Cryptography;

public interface IBCryptCryptographyHelper
{
    /// <summary>
    ///     Hash and salt the password using bcrypt
    /// </summary>
    /// <param name="valueToHash">Password in plain text</param>
    /// <param name="workFactor">Work factor to generate hash</param>
    /// <returns></returns>
    string HashWithBcrypt(string valueToHash, int workFactor);

    /// <summary>
    ///     Verify if valueToCompare is equivalent to hash
    /// </summary>
    /// <param name="valueToCompare">Value to compare in plain text</param>
    /// <param name="hash">Hashed value using Bcrypt</param>
    /// <returns></returns>
    bool VerifyBcryptHash(string valueToCompare, string hash);
}