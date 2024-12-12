namespace BusinessLogic.Repositories.Interfaces
{
    public interface IPostCommentRepository
    {
        void Add(DB.Entities.PostComment postComment);
        void Update(DB.Entities.PostComment postComment);
        DB.Entities.PostComment? Get(int id);
    }
}
