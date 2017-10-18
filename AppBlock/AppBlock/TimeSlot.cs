using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlock
{
    public class TimeSlot
    {
        private Time fromTime;
        private Time toTime;

        public TimeSlot(Time fromTime, Time toTime)
        {
            this.fromTime = fromTime;
            this.toTime = toTime;
        }

        #region setters and getters
        public Time getFromTime()
        {
            return fromTime;
      
        }

        public Time getToTime()
        {
            return toTime;

        }

        public void setFromTime(Time fromTime)
        {
            this.fromTime = fromTime;

        }

        public void setToTime(Time toTime)
        {
            this.toTime = toTime;

        }
        #endregion
    }
}
