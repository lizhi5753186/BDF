namespace Bdf.Configuration
{
    public class DictionayBasedConfig : IDictionaryBasedConfig
    {
        public void Set<T>(string name, T value)
        {
            throw new System.NotImplementedException();
        }

        public object Get(string name)
        {
            throw new System.NotImplementedException();
        }

        public T Get<T>(string name)
        {
            throw new System.NotImplementedException();
        }

        public object Get(string name, object defaultValue)
        {
            throw new System.NotImplementedException();
        }

        public T Get<T>(string name, T defaultValue)
        {
            throw new System.NotImplementedException();
        }

        public T GetOrCreate<T>(string name, System.Func<T> creator)
        {
            throw new System.NotImplementedException();
        }
    }
}