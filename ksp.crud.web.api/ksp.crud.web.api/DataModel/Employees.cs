using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ksp.crud.web.api.DataModel
{
    public class Employees
    {
        [Key]
        [Required]
        public Guid EmployId { get; set; }

        public string? Photo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string FirstLastName { get; set; }

        [MaxLength(100)]
        public string SecondLastName { get; set; }

        [MaxLength(300)]
        public string? FullName { get; set; }

        public double? Salary { get; set; }

        [Required]
        [DefaultValue(1)]
        public int? Status { get; set; }

        public DateTime? HiringDate { get; set; }

        [Required]
        [MaxLength(250)]
        public string Job { get; set; }

    }
}
