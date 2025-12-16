using System.Globalization;

namespace AS.Entities.Enums
{
    public enum DocumentType
    {
        News,
        Activity,
        Announcement,
        Slider,
        Units,
        Pages
    }

    public static class DocumentTypeExtensions
    {
        public static string AsString(this DocumentType val)
        {
            return val.ToString();
        }
     
    }
}
