using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdf.Timing
{
    public static class ClockManager
    {
        private static IClockProvider _provider;

        public static IClockProvider Provider
        {
            get { return _provider; }
            set
            {
                if (value == null)
                    throw new BdfException("Can not set Clock to null!");

                _provider = value;
            }
        }

        static ClockManager()
        {
            _provider = new LocalClockProvider();
        }

        public static DateTime Now
        {
            get { return Provider.Now; }
        }

        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }
    }
}
