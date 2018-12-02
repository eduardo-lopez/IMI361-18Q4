using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputacionMovilAPI.Models
{
    public partial class EventoWRK
    {
        [Key]
        public int EventoID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        public int InfanteID { get; set; }
        public int HitoID { get; set; }

        [ForeignKey("HitoID")]
        [InverseProperty("EventoWRK")]
        public HitoMSTR Hito { get; set; }
        [ForeignKey("InfanteID")]
        [InverseProperty("EventoWRK")]
        public InfanteMSTR Infante { get; set; }
    }
}
