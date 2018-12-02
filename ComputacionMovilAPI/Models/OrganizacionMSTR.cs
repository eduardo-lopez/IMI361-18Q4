using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputacionMovilAPI.Models
{
    public partial class OrganizacionMSTR
    {
        public OrganizacionMSTR()
        {
            PedriataMSTR = new HashSet<PedriataMSTR>();
        }

        [Key]
        public int OrganizacionID { get; set; }
        [StringLength(128)]
        public string OrganizacionNombre { get; set; }

        [InverseProperty("Organizacion")]
        public ICollection<PedriataMSTR> PedriataMSTR { get; set; }
    }
}
