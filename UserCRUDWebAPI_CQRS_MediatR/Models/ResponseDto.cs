namespace UserCRUDWebAPI_CQRS_MediatR.Models
{
    public record ResponseDto(dynamic Data, string ActionMessage, bool Status);

}
