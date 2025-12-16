using AS.Entities.Base;

namespace AS.Entities.Dtos
{
    public class RoleDto: Dto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }


        public int Level { get; set; }



    }
}
