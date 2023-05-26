namespace ERoseWebAPI.DTO.Responses
{
    public class LoginResponse : BaseResponse
    {
        public HeroResponse Hero { get; set; }
        public string Token { get; set; }
    }
}
