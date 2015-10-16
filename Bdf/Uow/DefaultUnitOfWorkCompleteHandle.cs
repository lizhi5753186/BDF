using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
namespace Bdf.Uow
{
    /// <summary>
    /// This handle is used for inner unit of work scopes.
    /// 
    /// </summary>
    public class DefaultUnitOfWorkCompleteHandle : IUnitOfWorkCompleteHandle
    {
        public const string DidNotCallCompleteMethodExceptionMessage = "Did not call Complete method of a unit of work.";

        private volatile bool _isCompleteCalled;
        private volatile bool _isDisposed;

        public void Complete()
        {
            _isCompleteCalled = true;
        }

        public async Task CompleteAsync()
        {
            _isCompleteCalled = true;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            if (_isCompleteCalled) return;
            if (HasException())
            {
                return;
            }

            throw new BdfException(DidNotCallCompleteMethodExceptionMessage);
        }

        private static bool HasException()
        {
            try
            {
                return Marshal.GetExceptionCode() != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}