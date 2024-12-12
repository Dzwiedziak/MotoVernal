using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
