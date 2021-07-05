using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecDesp.Domain.Models
{
    public class Transacao : BaseModel<long>
    {
        public DateTime Data { get; set; }
        public long AreaId { get; set; }
        public string Contraparte { get; set; }
        public double Valor { get; set; }
        public string descricao { get; set; }

        // corresponde ao tipo de transação: Receita = 1; Despesa = 2; Transferencia = 3; Cobrança = 4
        public int OrigemTipo { get; set; }
    }
}
