namespace BusinessLayer.BusinessHelper
{
    public interface ICodeHelper
    {
        public string GenerateVerificationCode(int length = 6);
    }
}