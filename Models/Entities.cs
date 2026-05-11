namespace AngularApp.BackendAPI.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class Designation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int DesignationId { get; set; }
        public int GenderId { get; set; }
    }
}
