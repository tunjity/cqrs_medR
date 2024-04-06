using System.ComponentModel.DataAnnotations;

namespace UserCRUDWebAPI_CQRS_MediatR.Entity
{
    public class School:BaseModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? LgaId { get; set; }
    }
}