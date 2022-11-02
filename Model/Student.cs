using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "NID is required.")]
        [StringLength(14, ErrorMessage = "The NID must contain 14 characters")]
        public string NID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        public string GuardianName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [StringLength(8, ErrorMessage = "The Phone Number must contain 8 characters")]
        public string PhoneNumber { get; set; }
        public List<StudentSubject> Subjects { get; set; }
        public int Marks { get; set; }

        public string Status { get; set; }
    }
}
