namespace RecDesp.Domain.Models
{
    public interface IBaseModel<T>
    {
        public T Id { get; set; }
    }
}
