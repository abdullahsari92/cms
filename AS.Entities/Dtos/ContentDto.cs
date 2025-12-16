using AS.Entities.Base;
using AS.Entities.Entity;
using AS.Entities.Enums;


namespace AS.Entities.Dtos
{
    public class ContentDto: Dto
    {
        public Guid UnitId { get; set; }
        public Units Units { get; set; }
        public ContentCategory ContentCategory { get; set; }

    }
}
