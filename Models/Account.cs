using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Crud_Operation.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [ForeignKey("user")]
        public int RoleId { get; set; } = 2;
        public UserRole user { get; set; }

    }
}
       