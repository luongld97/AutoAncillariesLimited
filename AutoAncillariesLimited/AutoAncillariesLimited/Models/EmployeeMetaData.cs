using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models
{
  public class EmployeeMetaData
  {
    [Display(Name = "New Password")]
    [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "Your password must have letters,uppercase ,numbers, and special characters")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Your password should be between 6-20 ")]
    public string Password { get; set; }


    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Confirm Password and password do not match")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Your name does not left blank")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be between 2-50")]
    [RegularExpression(@"^[a-zA-Z\s_-]*$", ErrorMessage = "Please type your name in the format")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Your address does not left blank")]
    [RegularExpression(@"^[a-zA-Z0-9\s,_-]*$", ErrorMessage = "Please type your address in the format")]
    [Display(Name = "Address")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Your email does not left blank")]
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Please type your email address in the format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Your phone does not left blank")]
    [Phone(ErrorMessage = "Please type your phone in the format")]
    [RegularExpression(@"^0[1-9].*$", ErrorMessage = "Please type your phone in the format")]
    [StringLength(11, MinimumLength = 10, ErrorMessage = "Your phone should be between 10-11 number")]
    [Display(Name = "Phone")]
    public string Phone { get; set; }
  }

  [MetadataType(typeof(EmployeeMetaData))]
  public partial class Employee
  {
      [NotMapped]
      public string ConfirmPassword { get; set; }
    }
}