using RecDesp.Models;
using System;

namespace RecDesp.Domain.Models
{
    public class Cobranca : BaseModel<long>
    {
        public DateTime Data { get; set; }
        public double Valor { get; set; }

        // 1 = pendente 2 = aprovado 3 = rejeitado
        public int Status { get; set; }
        public string Descricao { get; set; }
        public Area FromArea { get; set; }
        public long FromAreaId { get; set; }
        public Area ToArea { get; set; }
        public long ToAreaId { get; set; }

    }
}
