using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputacionMovilAPI.Models
{
    public partial class InfanteMSTR
    {
        public InfanteMSTR()
        {
            EventoWRK = new HashSet<EventoWRK>();
            InfanteHitoXREF = new HashSet<InfanteHitoXREF>();
        }

        [Key]
        public int InfanteID { get; set; }
        [StringLength(128)]
        public string InfanteNombre { get; set; }
        [StringLength(1)]
        public string Genero { get; set; }
        public int? PedriataID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FechaDeNacimiento { get; set; }
        [StringLength(128)]
        public string CorreoElectronico { get; set; }
        public bool? NotificarPorCorreo { get; set; }
        [Column(TypeName = "image")]
        public byte[] Foto { get; set; }
        public int? EdadMeses { get; set; }
        public decimal? EdadYear { get; set; }

        [ForeignKey("PedriataID")]
        [InverseProperty("InfanteMSTR")]
        public PedriataMSTR Pedriata { get; set; }
        [InverseProperty("Infante")]
        public ICollection<EventoWRK> EventoWRK { get; set; }
        [InverseProperty("Infante")]
        public ICollection<InfanteHitoXREF> InfanteHitoXREF { get; set; }
    }
}
