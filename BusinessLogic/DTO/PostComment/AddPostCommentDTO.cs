namespace BusinessLogic.DTO.PostComment
{
    public class AddPostCommentDTO
    {
        public string Content { get; set; }

        public AddPostCommentDTO()
        {
        }

        public AddPostCommentDTO(string content)
        {
            Content = content;
        }
    }
}
