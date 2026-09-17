namespace trabalho_web.Entities
{
    public class UserEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public UserEntity( string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }


    }
}
