namespace ArsaRExCH.StaticsHelper.CustomException
{
    public class AddPairException :Exception
    {
        public AddPairException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
        public AddPairException(string message)
       : base(message)
        {
        }
    }
}
