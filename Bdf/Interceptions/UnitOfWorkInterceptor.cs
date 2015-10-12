﻿using System.Threading.Tasks;
using Castle.DynamicProxy;
using Bdf.Threading;
using Bdf.Uow;

namespace Bdf.Interceptions
{
    /// <summary>
    /// Used to manager database connection and transaction.
    /// </summary>
    public class UnitOfWorkInterceptor : IInterceptor
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// Intercepts a method.
        /// </summary>
        /// <param name="invocation">Method invocation arguments</param>
        public void Intercept(IInvocation invocation)
        {
            if (_unitOfWorkManager.Current != null)
            {
                //Continue with current uow
                invocation.Proceed();
                return;
            }

            var unitOfWorkAttr = UnitOfWorkAttribute.GetUnitOfWorkAttributeOrNull(invocation.MethodInvocationTarget);
            if (unitOfWorkAttr == null || unitOfWorkAttr.IsDisabled)
            {
                //No need to a uow
                invocation.Proceed();
                return;
            }

            //No current uow, run a new one
            PerformUow(invocation, unitOfWorkAttr.CreateOptions());
        }

        private void PerformUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                PerformAsyncUow(invocation, options);
            }
            else
            {
                PerformSyncUow(invocation, options);
            }
        }

        private void PerformSyncUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            using (var uow = _unitOfWorkManager.Begin(options))
            {
                invocation.Proceed();
                uow.Complete();
            }
        }

        private void PerformAsyncUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            var uow = _unitOfWorkManager.Begin(options);

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                    (Task)invocation.ReturnValue,
                    async () => await uow.CompleteAsync(),
                    exception => uow.Dispose()
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    async () => await uow.CompleteAsync(),
                    (exception) => uow.Dispose()
                    );
            }
        }
    }
}