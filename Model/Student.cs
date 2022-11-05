using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Utility;

namespace Model
{
    public class Student
    {
        public int StudentId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "NID is required.")]
        [RegularExpression(@"(.{14})", ErrorMessage = "The NID must contain 14 characters")]
        public string NID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        public string GuardianName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string EmailAddress { get; set; }

        [MinimumAge(17, ErrorMessage ="Minimum Age is 17")]
        [MaximumAge(80, ErrorMessage = "Maximum Age is 80")]
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [StringLength(8, ErrorMessage = "The Phone Number must contain 8 characters")]
        public string PhoneNumber { get; set; }
        public List<StudentSubject> Subjects { get; set; }
        public int Marks { get; set; }
        public string Status { get; set; }
    }
}
