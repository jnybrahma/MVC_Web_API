using static DevaLokaVilla_Utility.SD;

namespace DevaLokaVilla_WebApp.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
