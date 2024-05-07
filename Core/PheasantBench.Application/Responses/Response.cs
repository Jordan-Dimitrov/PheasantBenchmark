namespace PheasantBench.Application.Responses
{
    public class Response
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public Response()
        {
            Success = true;
        }
    }
}
