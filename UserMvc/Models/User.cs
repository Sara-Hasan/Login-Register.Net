using System;
using System.ComponentModel.DataAnnotations;

namespace UserMvc.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Enter FullName")]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(200)]
        public byte[] passwordHash { get; set; }
        [Required]
        [StringLength(200)]
        public byte[] passwordSalt { get; set; }

        [Required(ErrorMessage = "Enter Mobile Number")]
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
       


    }
}
