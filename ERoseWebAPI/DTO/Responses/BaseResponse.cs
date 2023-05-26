using ERoseWebAPI.Models;

namespace ERoseWebAPI.DTO.Responses
{
    public class BaseResponse : ModelBase
    {
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
