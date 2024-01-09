using System.Threading.Tasks;
using System.Threading;

namespace Save.State
{
    public interface IState
    {
        public Task<string> ToStringAsync(CancellationToken ct);
    }
}