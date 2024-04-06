using System.ComponentModel.DataAnnotations;

namespace UserCRUDWebAPI_CQRS_MediatR.Entity
{
    public class BaseModel
    {
        [Key]
        public Guid ID { get; set; }=Guid.NewGuid();
        public DateTime? DateCreated { get; set; }=DateTime.Now;
        public int CreatedBy { get; set; }=66;
    }
}