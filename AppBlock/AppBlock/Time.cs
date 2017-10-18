using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBlock
{
    public class Time
    {
        private int hour;
        private int minute;
        private int second;

        public Time(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        #region getters and setters

        public String getTime()
        {
            string h = "";
            string m = "";
            string s = "";

            if (hour < 10)
            {
                h = "0" + hour.ToString();
            }
            else h = hour.ToString();
            if (minute < 10)
            {
                m = "0" + minute.ToString();
            }
            else m = minute.ToString();
            if (second < 10)
            {
                s = "0" + second.ToString();
            }
            else s = second.ToString();

            return h + ": " + m+ ": " + s;

        }

        public int getHour()
        {
            return hour;
        }

        public int getMinute()
        {
            return minute;
        }

        public int getSecond()
        {
            return second;
        }

        public void setHour(int hour)
        {
            this.hour = hour;
        }

        public void setMinute(int minute)
        {
            this.minute = minute;
        }

        public void setSecond(int second)
        {
            this.second = second;
        }
        #endregion

        public Time subtractTime(Time subtractedTime)
        {
            Time resultTime = new AppBlock.Time(0, 0, 0);


            if(hour<subtractedTime.getHour())
                resultTime.setHour(hour - subtractedTime.getHour() + 24);
            else
                resultTime.setHour(hour - subtractedTime.getHour());


            if (minute < subtractedTime.getMinute())
            {
                resultTime.setMinute(minute - subtractedTime.getMinute() + 60);
                resultTime.setHour(resultTime.getHour()-1);
            }
            else
                resultTime.setMinute(minute - subtractedTime.getMinute());

            if (second < subtractedTime.getSecond())
            {
                resultTime.setSecond(second - subtractedTime.getSecond() + 60);
                resultTime.setMinute(resultTime.getMinute() - 1);
            }
            else
                resultTime.setSecond(second - subtractedTime.getSecond());

        
            return resultTime;
        }

        public Time addTime(Time subtractedTime)
        {
            Time resultTime = new AppBlock.Time(0, 0, 0);


            if (minute + subtractedTime.getMinute() >=60)
            {
                resultTime.setMinute(minute + subtractedTime.getMinute() -60);
                resultTime.setHour(resultTime.getHour() + 1);
            }
            else
                resultTime.setMinute(minute + subtractedTime.getMinute());

            if (hour + subtractedTime.getHour() >= 24)
                resultTime.setHour(hour + subtractedTime.getHour() - 24);
            else
                resultTime.setHour(hour + subtractedTime.getHour());

            return resultTime;
        }

        public static String timeUntil(Time time)
        {//return the duration left until the provided time
            Time currentTime = new Time(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            return time.subtractTime(currentTime).getTime().ToString();

        }
    }
}
