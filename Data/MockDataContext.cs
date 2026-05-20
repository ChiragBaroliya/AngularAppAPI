using AngularApp.BackendAPI.Models;

namespace AngularApp.BackendAPI.Data
{
    public static class MockDataContext
    {
        public static List<Role> Roles { get; set; } = new List<Role>
        {
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" }
        };

        public static List<Designation> Designations { get; set; } = new List<Designation>
        {
            new Designation { Id = 1, Name = "Software Engineer" },
            new Designation { Id = 2, Name = "Manager" },
            new Designation { Id = 3, Name = "HR" },
            new Designation { Id = 4, Name = "Developer" },
        };

        public static List<Gender> Genders { get; set; } = new List<Gender>
        {
            new Gender { Id = 1, Name = "Male" },
            new Gender { Id = 2, Name = "Female" },
            new Gender { Id = 3, Name = "Other" }
        };

        public static List<User> Users { get; set; } = new List<User>
        {
            new User { Id = 1, FullName = "Admin User", Email = "admin@example.com", Password = "password123", RoleId = 1, DesignationId = 2, GenderId = 1 }
        };
    }
}
