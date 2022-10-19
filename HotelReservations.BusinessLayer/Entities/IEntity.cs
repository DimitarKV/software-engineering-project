namespace HotelReservations.BusinessLayer.Entities
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
