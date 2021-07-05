using RecDesp.Models;
using System;

namespace RecDesp.Domain.Models
{
    public class Credito : BaseModel<long>
    {
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public string ExternalName { get; set; }
        public int ExternalId { get; set; }
        public string Descricao { get; set; }
        public Area Area { get; set; }
        public long AreaId { get; set; }
    }
}
