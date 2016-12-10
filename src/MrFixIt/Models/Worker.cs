using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MrFixIt.Models
{           //name of our Table Worker
    public class Worker
    {
        //table name needs to be present.
        [Key]
        public int WorkerId { get; set; }
        //primary key is worker Id.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Avaliable { get; set; }
        public string UserName { get; set; }
        //this comes from Identity.User
        public virtual ICollection<Job> Jobs { get; set; }
        // virtual keyword again allows to link jobs with this worker table.
        

        
        public Worker()
        {
            // if jobs are are avaliabe it allows a worker to get that job, keyword is true.
            Avaliable = true;
        }

    }
}