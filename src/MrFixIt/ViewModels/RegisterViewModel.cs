using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrFixIt.ViewModels
// ViewModel is data the shows up only in the view. We use ViewModel instead of a regular Model.
// because we dont want to create a new table in the database to represent the data.


//this is  a data model we can use for user registration.

// This is gonna be the layout in the register file in the views folder.
{
    public class RegisterViewModel
    {
        
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        //propetires 
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
