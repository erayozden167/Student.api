using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models.Entity
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(25)]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
    }
}
