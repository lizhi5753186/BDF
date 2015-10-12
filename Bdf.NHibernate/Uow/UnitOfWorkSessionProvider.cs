using NHibernate;
using Bdf.Dependency;
using Bdf.Uow;

namespace Bdf.NHibernate.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWorkSessionProvider : ISessionProvider, ITransientDependency
    {
        public ISession Session
        {
            get { return _unitOfWorkProvider.Current.GetSession(); }
        }

        private readonly ICurrentUnitOfWorkProvider _unitOfWorkProvider;

        public UnitOfWorkSessionProvider(ICurrentUnitOfWorkProvider unitOfWorkProvider)
        {
            _unitOfWorkProvider = unitOfWorkProvider;
        }
    }
}