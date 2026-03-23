using System.ComponentModel.DataAnnotations;
namespace customerOrders.Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public bool IsCanceled { get; set; }
    }
}
