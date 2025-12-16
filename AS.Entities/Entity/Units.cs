using AS.Entities.Base;


namespace AS.Entities.Entity
{
    public class Units: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
        public string WhatshappNumber { get; set; }
        public Guid LogoDocumentId { get; set; } //update kullanılacak
        public Guid? LogoTwoDocumentId { get; set; } //update kullanılacak
        public Document? LogoDocument { get; set; } //update kullanılacak
        public Document? LogoTwoDocument { get; set; } //update kullanılacak
        public string Description { get; set; }
        public string Theme { get; set; }
        public string Color { get; set; }


        public ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Content> Contents { get; set; } //Bir birimin birden fazla Contenti olabilir.
        public virtual ICollection<RoleUserLine> RoleUserLineList { get; set; }
        public virtual ICollection<Person> PersonList { get; set; }
        public ICollection<Slider> SliderList { get; set; }
    }
}
