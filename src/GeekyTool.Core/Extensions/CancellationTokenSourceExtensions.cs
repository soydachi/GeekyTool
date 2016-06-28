using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GeekyTool.Extensions
{
    public static class CancellationTokenSourceExtensions
    {
        public static void ResetIfCancelled(this CancellationTokenSource cts)
        {
            if (cts.IsCancellationRequested)
                cts = new CancellationTokenSource();
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