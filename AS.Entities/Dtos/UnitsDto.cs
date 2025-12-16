using AS.Entities.Base;
using AS.Entities.Entity;


namespace AS.Entities.Dtos
{
    public class UnitsDto:Dto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
        public string? WhatshappNumber { get; set; }
        public Guid LogoDocumentId { get; set; } //update kullanılacak
        public Guid? LogoTwoDocumentId { get; set; } //update kullanılacak
        public string Description { get; set; }
        public string? Theme { get; set; }
        public string? Color { get; set; }

        public DocumentSummeryDto? LogoDocumentSummery { get; set; } 
        public DocumentSummeryDto? LogoTwoDocumentSummery { get; set; }

    }
}
