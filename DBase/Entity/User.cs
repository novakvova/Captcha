using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBase.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength: 255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required,StringLength(maximumLength:200)]
        public string Password { get; set; }

        [Required, StringLength(maximumLength: 150)]
        public string PasswordSalt { get; set; }

        public Guid MinPhoto { get; set; }
        public Guid Photo { get; set; }
        public Guid LargePhoto { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
