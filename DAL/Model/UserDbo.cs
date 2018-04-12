using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    public class UserDbo
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool EmailIsValidate { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public Guid ActivationCode { get; set; }

        public virtual ICollection<CommentDbo> UserComments { get; set; }
    }
}
