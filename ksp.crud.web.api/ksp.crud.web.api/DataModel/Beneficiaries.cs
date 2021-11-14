using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ksp.crud.web.api.DataModel
{
    public class Beneficiaries
    {
        [Key]
        [Required]
        public Guid BeneficiaryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? FirstLastName { get; set; }

        [MaxLength(100)]
        public string? SecondLastName { get; set; }

        [MaxLength(300)]
        public string? FullName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Relationship { get; set; }

        public DateTime? DateBirth { get; set; }

        public int? Gender { get; set; }

        [Required]
        [DefaultValue(1)]
        public int Status { get; set; }

        [Required]
        public Guid EmployId { get; set; }

    }
}
