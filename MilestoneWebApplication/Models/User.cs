
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MilestoneWebApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name: ")]
        [StringLength(50, MinimumLength = 3)]
        public String firstName { get; set; }
        [Required]
        [DisplayName("Last Name: ")]
        [StringLength(50, MinimumLength = 3)]
        public String lastName { get; set; }
        [Required]
        [DisplayName("Sex: ")]
        [StringLength(50, MinimumLength = 3)]
        public String sex { get; set; }
        [Required]
        [DisplayName("Age: ")]
        public int age { get; set; }
        [Required]
        [DisplayName("State: ")]
        public String state { get; set; }
        [Required]
        [DisplayName("Email: ")]
        public String email { get; set; }
        [Required]
        [DisplayName("Username: ")]
        public String username { get; set; }
        [Required]
        [DisplayName("Password: ")]
        public String password { get; set; }

        public List<Board> savedGames { get; set; }

        public User()
        {
        }
        public User(int id, string firstName, string lastName, string sex, int age, string state, string email, string username, string password, List<Board> savedGames)
        {
            Id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.sex = sex;
            this.age = age;
            this.state = state;
            this.email = email;
            this.username = username;
            this.password = password;
            this.savedGames = savedGames;
        }

        public override string ToString()
        {
            return firstName;
        }
    }
}
