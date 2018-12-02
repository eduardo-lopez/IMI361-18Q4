using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputacionMovilAPI.Models
{
    public partial class PedriataMSTR
    {
        public PedriataMSTR()
        {
            InfanteMSTR = new HashSet<InfanteMSTR>();
        }

        [Key]
        public int PedriataID { get; set; }
        [StringLength(128)]
        public string PedriataNombre { get; set; }
        [StringLength(128)]
        public string CorreoElectronico { get; set; }
        public bool? NotificarPorCorreo { get; set; }
        public int? OrganizacionID { get; set; }

        [ForeignKey("OrganizacionID")]
        [InverseProperty("PedriataMSTR")]
        public OrganizacionMSTR Organizacion { get; set; }
        [InverseProperty("Pedriata")]
        public ICollection<InfanteMSTR> InfanteMSTR { get; set; }
    }
}
