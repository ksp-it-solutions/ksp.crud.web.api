using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ksp.crud.web.api.DataModel
{
    public class SystemUsers
    {
        [Key]
        [Required]
        public Guid SystemuserId { get; set; }

        [Required]
        [MaxLength(50)]
	    public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }

        [Required]
        public Guid EmployId { get; set; }

        public Employees Employ { get; set; }
    }
}
