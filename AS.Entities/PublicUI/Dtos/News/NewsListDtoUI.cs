using AS.Entities.Dtos;
using AS.Entities.Enums;
using AS.Entities.PublicUI.Base;

namespace AS.Entities.PublicUI.Dtos.News
{
    public class NewsListDtoUI:DtoUI
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public NewsType NewsType { get; set; }

        public DocumentSummeryDto CoverImage { get; set; }
        
    }
}
