using AS.Entities.Base;

namespace AS.Entities.Dtos
{
    public class UserDto: Dto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName => FirstName + " " + LastName;
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public int UserType { get; set; }


        public List<string> RoleIds { get; set; }




    }
}
