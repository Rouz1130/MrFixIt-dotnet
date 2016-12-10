using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ViewMoel is data the shows up only in the view. We use ViewModel instead of a regular Model because we dont want to create a new table in the database to represent the data.

namespace MrFixIt.ViewModels
{
    // were allwing users to sign in. with email and password.
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
