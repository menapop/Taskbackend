namespace FoodSys.Service.Helper.Password
{
  
    public interface IPasswordHandler
    {
        
        string CreatePasswordHash(string pwd);
        bool Validate(string password, string passwordHash);
        int GenerateRandomNumber();
    }
}
