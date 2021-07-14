using RecDesp.Models;
using System;

namespace RecDesp.Domain.Models
{
    public class Transferencia : BaseModel<long>
    {
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public long FromAreaId { get; set; }
        public long ToAreaId { get; set; }

        public Area FromArea { get; set; }
        public Area ToArea { get; set; }
    }
}
