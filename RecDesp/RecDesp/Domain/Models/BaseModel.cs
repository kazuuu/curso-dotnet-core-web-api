using System.ComponentModel.DataAnnotations.Schema;

namespace RecDesp.Domain.Models
{
    public class BaseModel<T> : IBaseModel<T>
    {
        [Column("id")]
        public T Id { get; set; }
    }
}
