using System.Threading.Tasks;
using System.Threading;

namespace Save
{
    public interface IState
    {
        public Task<string> ToStringAsync(CancellationToken ct);
    }
}