namespace HotelReservations.Data.Entities
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
