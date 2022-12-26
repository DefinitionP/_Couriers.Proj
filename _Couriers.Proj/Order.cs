using System;
using System.Collections.Generic;
using System.Linq;

namespace _Couriers.Proj
{
    public class Order
    {
        /// <summary>
        /// Список всех заказов компании
        /// </summary>
        public static List<Order> OrderQueue = new List<Order>();
        /// <summary>
        ///  Начальная точка маршрута заказа
        /// </summary>
        public Location From { get; set; }
        /// <summary>
        /// Конечная точка маршрута заказа
        /// </summary>
        public Location To { get; set; }
        /// <summary>
        /// Тип заказа
        /// </summary>
        public bool IsDelivery { get; set; }
        /// <summary>
        /// Масса груза
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Принесёт ли заказ прибыль компании
        /// </summary>
        public bool IsProfitable { get; set; }
        /// <summary>
        /// Время, необходимое курьеру для выполнения заказа (в секундах внутреннего времени программы)
        /// </summary>
        public double RequiredTime { get; set; }
        /// <summary>
        /// Статус заказа
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Интервал времени, за который нужно выполнить заказ
        /// </summary>
        public TimeSpan OrderTime { get; set; }
        /// <summary>
        /// Цена, которую платит заказчик
        /// </summary>
        public double CustomerPayment { get; set; }
        public string DistanceLeft { get; set; }
        public DateTime StartOrderTime { get; set; }
        public DateTime TimeCheckPoint { get; set; }
        public Couriers SelectrdCourier { get; set; }
        public double FinalProfit { get; set; }
        public bool IsDelivrrED { get; set; }
        public static double GenerateCustomerPayment()
        {
            int maxDelta = (int)Math.Ceiling(0.35 * Program.AverageCustomerPayment);
            Random rnd = new Random();
            int delta = rnd.Next(-maxDelta, maxDelta);

            double CustomerPayment = Program.AverageCustomerPayment + delta;
            return CustomerPayment;
        }
        public static TimeSpan GenerateOrderTime()
        {
            double TimeMultiplier = Program.OrderTimeMultiplier;
            Random rnd = new Random();
            int maxDelta = (int)(Program.AverageOrderTime * TimeMultiplier * 60);
            int delta = 0;
            // Время заказа не должно быть больше времени смены
            do
            {
                delta = rnd.Next(-maxDelta, maxDelta);
            }
            while (((Program.AverageOrderTime + delta / 60) >= Program.ShiftDurationPerVirtualHours));
            TimeSpan opdertime = new TimeSpan(Program.AverageOrderTime, delta, 0);
            return opdertime;

        }
        public static int GenerateWeight()
        {
            int maxDelta = (int)Math.Ceiling(0.4 * Program.AverageWeight);
            Random rnd = new Random();
            int delta = rnd.Next(-maxDelta, maxDelta);

            int weight = Program.AverageWeight + delta;
            return weight;
        }
    }


    public class Delivery : Order
    {
        public Delivery()
        {

            From = Location.Create();
            To = Location.Create();
            IsDelivery = true;
            CustomerPayment = GenerateCustomerPayment();
            OrderTime = GenerateOrderTime();
            Weight = GenerateWeight();
            Status = -1;
            DistanceLeft = "курьер не назначен";
            IsDelivrrED = false;
            TimeCheckPoint = Time.timelist.Last().time;
            OrderQueue.Add(this);

        }
    }
    public class Pickup : Order
    {
        public Pickup()
        {

            From = Location.Create();
            To = Location.Create();
            IsDelivery = false;
            CustomerPayment = GenerateCustomerPayment();
            OrderTime = GenerateOrderTime();
            Weight = GenerateWeight();
            Status = -1;
            DistanceLeft = "курьер не назначен";
            IsDelivrrED = false;
            TimeCheckPoint = Time.timelist.Last().time;
            OrderQueue.Add(this);

        }
    }
}

