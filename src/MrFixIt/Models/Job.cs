using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrFixIt.Models
{
    // Model /Table for our database.
    // class job is our table name
    public class Job
    {
        // Table names are not present?.
        [Key]   // this is representing the primary key in the table public in JobId
        public int JobId { get; set; }
        // Columns in our table , Title, Description, Completed, Pending, Active.
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public bool Pending { get; set; }
        public bool Active { get; set; }
        // Virtual is too link Worker wiith job tables.
        public virtual Worker Worker { get; set; }
    }
}
