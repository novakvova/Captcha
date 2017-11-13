using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBase.Entity
{
    public class UserProfile
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        [Required, StringLength(maximumLength:100)]
        public string LastName { get; set; }
        [Required, StringLength(maximumLength: 100)]
        public string Name { get; set; }
        [Required, StringLength(maximumLength: 100)]
        public string SecondName { get; set; }
        public virtual User User { get; set; }
    }
}
