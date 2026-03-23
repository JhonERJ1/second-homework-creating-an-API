namespace customerOrders.API.Models.Dtos
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsCanceled { get; set; }
    }
}
