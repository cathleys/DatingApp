using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;
public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    Task<PageList<LikeDto>> GetUserLikes(LikesParams likesParams);

}
