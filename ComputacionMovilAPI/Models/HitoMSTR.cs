using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputacionMovilAPI.Models
{
    public partial class HitoMSTR
    {
        public HitoMSTR()
        {
            EventoWRK = new HashSet<EventoWRK>();
            InfanteHitoXREF = new HashSet<InfanteHitoXREF>();
        }

        [Key]
        public int HitoID { get; set; }
        [StringLength(128)]
        public string HitoDescripcion { get; set; }
        public int? ParamNumEntero01Default { get; set; }

        [InverseProperty("Hito")]
        public ICollection<EventoWRK> EventoWRK { get; set; }
        [InverseProperty("Hito")]
        public ICollection<InfanteHitoXREF> InfanteHitoXREF { get; set; }
    }
}
