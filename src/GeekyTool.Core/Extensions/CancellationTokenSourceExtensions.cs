using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GeekyTool.Extensions
{
    public static class CancellationTokenSourceExtensions
    {
        public static CancellationTokenSource ResetIfCancelled(this CancellationTokenSource cts)
        {
            if (cts.IsCancellationRequested)
                return new CancellationTokenSource();
            else
                return cts;
        }

        public static CancellationTokenSource JoinWith(this CancellationToken ct, CancellationToken otherToken)
        {
            return CancellationTokenSource.CreateLinkedTokenSource(ct, otherToken);
        }

        public static CancellationTokenSource JoinWith(this CancellationToken ct, IList<CancellationToken> otherTokens)
        {
            otherTokens.Add(ct);
            return CancellationTokenSource.CreateLinkedTokenSource(otherTokens.ToArray());
        }
    }
}