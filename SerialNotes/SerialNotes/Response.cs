namespace SerialNotes
{
    public class Response : IResponse
    {
        public int Status { get; set; }
        public string RenderHtml { get; set; }

        public Response()
        {

        }

        public Response(int status, string renderHtml)
        {
            Status = status;
            RenderHtml = renderHtml;
        }
    }
}
