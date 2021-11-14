using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ksp.crud.web.api.DataModel
{
    public class Token
    {
        [Key]
        [Required]
        public Guid TokenId { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        [DefaultValue(0)]
        public int IsExpirated { get; set; }
    }
}
