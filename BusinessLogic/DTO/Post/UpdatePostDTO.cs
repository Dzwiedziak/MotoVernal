namespace BusinessLogic.DTO.Post
{
    public class UpdatePostDTO
    {
        public string Content { get; set; }

        public UpdatePostDTO(string content)
        {
            Content = content;
        }
    }
}
