using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SulsApp.Data.Model
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Submissions = new List<Submission>();
        }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
