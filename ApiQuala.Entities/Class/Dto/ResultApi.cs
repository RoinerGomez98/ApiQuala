namespace ApiQuala.Entities.Class.Dto
{
    public class ResultApi
    {
        public string Message { get; set; }
        public bool Result { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
