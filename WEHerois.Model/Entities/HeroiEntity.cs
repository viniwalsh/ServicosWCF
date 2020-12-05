using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEHerois.Model.Entities
{
    [Table("Heroi")]
    public class HeroiEntity
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Codinome { get; set; }

        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }

        public string Poder { get; set; }

        [DisplayName("Foto")]
        public string ImageUri { get; set; }

        public DateTime? UltimaVisualizacao { get; set; }

        public int QuantidadeVisualizacao { get; set; }
    }
}
