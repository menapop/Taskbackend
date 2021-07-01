namespace FoodSys.Service.Helper.TripleDES
{
    public interface ITripleDES
    {
        string Encrypt(string toEncrypt, bool useHashing = true);
        string Decrypt(string cipherString, bool useHashing = true);
        string GenerateRandomPassword();
    }
}
