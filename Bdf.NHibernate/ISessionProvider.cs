using NHibernate;

namespace Bdf.NHibernate
{
    public interface ISessionProvider
    {
        ISession Session { get; }
    }
}