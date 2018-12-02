using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputacionMovilAPI.Models
{
    public partial class InfanteHitoXREF
    {
        public int InfanteID { get; set; }
        public int HitoID { get; set; }
        public int MaxEventos { get; set; }

        [ForeignKey("HitoID")]
        [InverseProperty("InfanteHitoXREF")]
        public HitoMSTR Hito { get; set; }
        [ForeignKey("InfanteID")]
        [InverseProperty("InfanteHitoXREF")]
        public InfanteMSTR Infante { get; set; }
    }
}
