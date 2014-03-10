using HashBot.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HashBot.Core.Services
{
    public interface ITwitterService
    {
        Task<IEnumerable<TweetModel>> Search(TagModel tag);
    }
}
