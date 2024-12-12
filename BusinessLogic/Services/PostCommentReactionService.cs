using BusinessLogic.DTO.PostCommentReaction;
using BusinessLogic.Repositories;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PostCommentReactionService : IPostCommentReactionService
    {
        readonly IPostCommentReactionRepository _postCommentReactionRepository;
        public PostCommentReactionService(IPostCommentReactionRepository postCommentReactionRepository)
        {
            _postCommentReactionRepository = postCommentReactionRepository;
        }
        public void AddOrUpdate(PostCommentReactionDTO postCommentReaction)
        {
            List<PostCommentReaction> dbPostCommentReactions = _postCommentReactionRepository.GetAll();
            PostCommentReaction? userReactionOnSpecificComment = 
                dbPostCommentReactions.Where(ur => ur.User.Id == postCommentReaction.User.Id)
                .Where(ur => ur.PostComment.Id == postCommentReaction.PostComment.Id)
                .FirstOrDefault();
            if(userReactionOnSpecificComment != null)
            {
                UpdatePostCommentReaction(ref userReactionOnSpecificComment, postCommentReaction);
                _postCommentReactionRepository.Update(userReactionOnSpecificComment);
            }
            else
            {
                var postCommentReactionToAdd = CreatePostCommentReaction(postCommentReaction);
                _postCommentReactionRepository.Add(postCommentReactionToAdd);
            }
        }
        public PostCommentReaction CreatePostCommentReaction(PostCommentReactionDTO postCommentReaction)
        {
            return new PostCommentReaction(0, postCommentReaction.ReactionType, postCommentReaction.User, postCommentReaction.PostComment);
        }
        public void UpdatePostCommentReaction(ref PostCommentReaction postCommentReactionToUpdate, PostCommentReactionDTO postCommentReactionDTO)
        {
            postCommentReactionToUpdate.ReactionType = postCommentReactionDTO.ReactionType;
        }
    }
}
