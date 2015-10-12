namespace Bdf.Uow
{
    public class DefaultUnitOfWorkCompleteHandle : IUnitOfWorkCompleteHandle
    {

        public void Complete()
        {
            throw new System.NotImplementedException();
        }

        public System.Threading.Tasks.Task CompleteAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}