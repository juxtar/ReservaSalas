using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using ReservaSalas.Modelos;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class UserModel : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        
        public int? Empleado_ID { get; set; }

        [ForeignKey("Empleado_ID")]
        public Empleado Empleado { get; set; }
    }
}