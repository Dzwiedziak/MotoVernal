using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
