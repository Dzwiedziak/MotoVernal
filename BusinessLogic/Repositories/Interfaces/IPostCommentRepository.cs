using DB.Entities;
using DB.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IPostCommentRepository
    {
        void Add(DB.Entities.PostComment postComment);
        void Update(DB.Entities.PostComment postComment);
        DB.Entities.PostComment Get(int id);
    }
}
