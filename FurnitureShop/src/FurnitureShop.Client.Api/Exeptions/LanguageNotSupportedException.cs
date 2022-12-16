namespace FurnitureShop.Client.Api.Exeptions
{
    public class LanguageNotSupportedException : Exception
    {
        public LanguageNotSupportedException() : base("Only 'uz', 'en' supported!")
        {

        }
    }
}
