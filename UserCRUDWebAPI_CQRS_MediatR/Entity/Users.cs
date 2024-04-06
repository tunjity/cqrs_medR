using System.ComponentModel.DataAnnotations;

namespace UserCRUDWebAPI_CQRS_MediatR.Entity
{
    public class Users:BaseModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
