using System;
using System.Collections.Generic;
using Bdf.Collections.Extensions;

namespace Bdf.Configuration
{
    /// <summary>
    /// Used to set/get custom configuration.
    /// </summary>
    public class DictionayBasedConfig : IDictionaryBasedConfig
    {
        /// <summary>
        /// Dictionary of custom configuration.
        /// </summary>
        protected Dictionary<string, object> CustomSettings { get; private set; }

        /// <summary>
        /// Gets/sets a config value.
        /// Returns null if no config with given name.
        /// </summary>
        /// <param name="name">Name of the config</param>
        /// <returns>Value of the config</returns>
        public object this[string name]
        {
            get { return CustomSettings.GetOrDefault(name); }
            set { CustomSettings[name] = value; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected DictionayBasedConfig()
        {
            CustomSettings = new Dictionary<string, object>();
        }

        /// <summary>
        /// Used to set a string named configuration.
        /// If there is already a configuration with same <paramref name="name"/>, it's overwritten.
        /// </summary>
        /// <typeparam name="T">Type of the config</typeparam>
        /// <param name="name">Unique name of the config</param>
        /// <param name="value">Value of the config </param>
        public void Set<T>(string name, T value)
        {
            this[name] = value;
        }

        /// <summary>
        /// Gets a configration object with given name.
        /// </summary>
        /// <param name="name">Unique Name of the config</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public object Get(string name)
        {
            return Get(name, null);
        }

        /// <summary>
        /// Get a configuration value as a specific type.
        /// </summary>
        /// <typeparam name="T">Type of the config</typeparam>
        /// <param name="name">Name of the config</param>
        /// <returns>Value of the config or null if not found</returns>
        public T Get<T>(string name)
        {
            var value = this[name];
            return value == null
                ? default(T)
                : (T) Convert.ChangeType(value, typeof (T));
        }

        /// <summary>
        /// Get a configuration object with given name.
        /// </summary>
        /// <param name="name">Unique name of the configuration</param>
        /// <param name="defaultValue">Default value of the object</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public object Get(string name, object defaultValue)
        {
            var value = this[name];
            return value ?? defaultValue;
        }

        /// <summary>
        /// Get a configuration object as a specific type 
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="name">Unique name of the config</param>
        /// <param name="defaultValue">Default value of the object</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public T Get<T>(string name, T defaultValue)
        {
            return (T) Get(name, (object) defaultValue);
        }

        /// <summary>
        /// Gets a configuration object with given name
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="name">Unique name of the config</param>
        /// <param name="creator">The function that will be called to create if given configuration is not found</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public T GetOrCreate<T>(string name, Func<T> creator)
        {
            var value = Get(name);
            if (value != null) return (T) value;

            value = creator();
            Set(name,value);
            return (T) value;
        }
    }
}