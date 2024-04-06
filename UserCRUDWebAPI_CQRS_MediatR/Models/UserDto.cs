using System.Text.Json.Serialization;

namespace UserCRUDWebAPI_CQRS_MediatR.Models
{
    public class UserDto
    {
        public Guid UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? DateCreated { get; set; }
        public int CreatedBy { get; set; }
    }
    public class SchoolDto
    { 
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? LgaId { get; set; }
         public DateTime? DateCreated { get; set; }
        public int CreatedBy { get; set; }
    }
}
