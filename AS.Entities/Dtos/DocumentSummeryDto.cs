using AS.Entities.Base;

namespace AS.Entities.Dtos
{
    public class DocumentSummeryDto
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }

        public string? Keyword { get; set; }
        public string? Base64 { get; set; }
        public bool IsDeleted { get; set; }

    }
}
