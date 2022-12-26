using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _Couriers.Proj
{
    public class Time
    {
        static int sleep, delta;
        public static List<Time> timelist = new List<Time>();
        public static List<Time> TimeCounters = new List<Time>();
        public static List<Time> LocationCounters = new List<Time>();
        public DateTime time { get; set; }
        public Time()
        {
            time = DateTime.Now;
            timelist.Add(this);
        }
        public Time(int q)
        {
            if (q == 1)
            {
                TimeCounters.Clear();
                time = timelist.Last().time;
                TimeCounters.Add(this);
            }
            if (q == 2)
            {
                LocationCounters.Clear();
                time = timelist.Last().time;
                LocationCounters.Add(this);
            }
        }

        /// <summary>
        /// Запуск таймера
        /// </summary>
        public static void Run()
        {
            Time CurrentTime = new Time();
            TimeCalculation(Program.TimeAcceleration);

            TimeSpan Shift = new TimeSpan((int)Program.ShiftDurationPerVirtualHours, 0, 0);
            TimeSpan Duration = timelist.Last().time - timelist[0].time;

            while ((Program.WakeUp == false) && (Duration < Shift))
            {
                Thread.Sleep(sleep);
                CurrentTime.time = CurrentTime.time.AddMilliseconds(delta * 1000);
                Duration = CurrentTime.time - timelist[0].time;
            }
            Program.WakeUp = true;
        }


        /// <summary>
        /// Метод, проводящий расчёты режима работы таймера (в зависимости от входных параметров, задаваемых в файле Program)
        /// </summary>
        /// <param name="accel"></param>
        static void TimeCalculation(int accel)
        {
            double minValue = Math.Ceiling(accel / 60f);
            while (Math.Truncate(accel / minValue) != (accel / minValue))
            {
                minValue++;
            }
            delta = (int)minValue;
            sleep = accel / delta;
        }

    }
}
