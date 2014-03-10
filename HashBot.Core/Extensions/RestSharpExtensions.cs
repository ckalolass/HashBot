using RestSharp;
using System.Threading.Tasks;

namespace HashBot.Core.Extensions
{
    public static class RestSharpExtensions
    {
        public static Task<IRestResponse> GetResponseContentAsync(this IRestClient client, IRestRequest request)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
        public static Task<IRestResponse<T>> GetResponseContentAsync<T>(this IRestClient client, IRestRequest request) where T : new ()
        {
            var tcs = new TaskCompletionSource<IRestResponse<T>>();
            client.ExecuteAsync<T>(request, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
