using System;
using System.Collections.Generic;
using System.Text;

namespace core
{
    public class Time
    {
        private int hours;
        private int minutes;
        private int seconds;

        public Time()
        {
            hours = 0;
            minutes = 0;
            seconds = 0;
        }

        public Time(int sec)
        {
            if (sec > 0)
            {
                hours = sec / 3600;
                minutes = (sec / 60) % 60;
                seconds = sec % 60;
                if (minutes == 60)
                {
                    hours = hours + 1;
                    minutes = 0;
                }
                if (seconds == 60)
                {
                    minutes = minutes + 1;
                    seconds = 0;
                }
            }
            else
            {
                hours = 0;
                minutes = 0;
                seconds = 0;
            }
        }

        public void setHours(int hours)
        {
            this.hours = hours;
        }

        public void setMinutes(int minutes)
        {
            this.minutes = minutes;
        }

        public void setSeconds(int seconds)
        {
            this.seconds = seconds;
        }

        public int getHours()
        {
            return hours;
        }

        public int getMinutes()
        {
            return minutes;
        }

        public int getSeconds()
        {
            return seconds;
        }

        public int timeToseconds()
        {
            int totalseconds = 0;
            if (hours > 0)
            {
                totalseconds = hours * 3600;
            }

            if (minutes > 0)
            {
                totalseconds = totalseconds + (minutes * 60);
            }

            if (seconds > 0)
            {
                totalseconds += seconds;
            }
            return totalseconds;
        }

        public override string ToString()
        {
            return hours.ToString() + " hr " + minutes.ToString() + " min " + seconds.ToString() + " sec";
        }
    }  
}
