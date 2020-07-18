using Quiz.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Data.Model.System
{
    public class Role : Superior
    {
        public string Name { get; set; }

        [NotMapped]
        public virtual List<RoleSystemAction> RoleSystemActions { get; set; }
    }
}