using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Student : EntityBase
    {
        public new int Id { get; set; }

        [Required(ErrorMessage = "NID is required.")]
        [StringLength(10, ErrorMessage = "The NID must contain 14 characters", MinimumLength = 14)]
        public string NID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Guardian Name is required.")]
        public string GuardianName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [StringLength(10, ErrorMessage = "The Phone Number must contain 8 characters", MinimumLength = 8)]
        public string PhoneNumber { get; set; }

        public List<StudentSubject> Subjects { get; set; }
    }
}
