namespace BusinessLogic.DTO.PostComment
{
    public class UpdatePostCommentDTO
    {
        public string Content { get; set; }

        public UpdatePostCommentDTO()
        {
        }

        public UpdatePostCommentDTO(string content)
        {
            Content = content;
        }
    }
}
