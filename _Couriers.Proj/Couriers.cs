using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _Couriers.Proj
{
    public abstract class Couriers
    {
        public static List<string> names = new List<string> { "Вася", "Петя", "Коля", "Нина", "Полина",
            "Ангелина", "Виталя", "Райан Гослинг", "Стёпа", "Григорий", "Гоша" };

        /// <summary>
        /// Список заказов в персональном расписании курьера
        /// </summary>
        public List<Order> CourierPlane = new List<Order>();
        public string Name { get; set; }
        /// <summary>
        /// Грузоподъёмность курьера, кг
        /// </summary>
        public int CarryingCapacity { get; set; }
        /// <summary>
        /// Скорость курьера в км/ч
        /// </summary>
        public double Speed { get; set; }
        /// <summary>
        /// Местоположение курьера
        /// </summary>
        public Location StartLocation { get; set; }
        /// <summary>
        /// Выплата курьеру за километр пути (после рассчётов)
        /// </summary>
        public double PaymentPerDistance { get; set; }
        /// <summary>
        /// Строка типа курьера, используется для вывода информации
        /// </summary>
        public string Type { get; set; }
        //public Location CurrentLocation { get; set; }
        /// <summary>
        /// Статус пути курьера
        /// </summary>
        public int CourierStatus { get; set; }
        public int direction { get; set; }

        internal static string GenerateName()
        {
            Random rnd = new Random();
            int q = rnd.Next(0, names.Count);
            return names[q];
        }
        internal static int GenerateCapacity()
        {
            int maxDelta = (int)Math.Ceiling(0.3 * Program.AverageCapacity);
            Random rnd = new Random();
            int delta = rnd.Next(-maxDelta, maxDelta);
            int capacity = Program.AverageCapacity + delta + 3;

            return capacity;
        }
        internal static int GenerateSpeed(string type)
        {
            if (type == Program.MobileType)
            {
                int maxDelta = (int)Math.Ceiling(Program.SpeedMultiplier * Program.MobileCourrierSpeed);
                Random rnd = new Random();
                Thread.Sleep(5);
                int speed = 0;
                while (speed <= 0)
                {
                    int delta = rnd.Next(-maxDelta, maxDelta);
                    speed = Program.MobileCourrierSpeed + delta;
                }
                return speed;
            }
            if (type == Program.FootType)
            {
                int maxDelta = (int)Math.Ceiling(Program.SpeedMultiplier * Program.FootCourrierSpeed);
                Random rnd = new Random();
                Thread.Sleep(5);
                int delta = rnd.Next(-maxDelta, maxDelta);
                int speed = Program.FootCourrierSpeed + delta;
                return speed;
            }
            else return 0;
        }
    }

    internal class MobileCourier : Couriers
    {
        public double Multiplier = Program.MobileCostMultiplier;
        public MobileCourier()
        {
            Type = Program.MobileType;
            Name = Couriers.GenerateName();
            CarryingCapacity = Couriers.GenerateCapacity();
            Speed = Couriers.GenerateSpeed(Type);
            StartLocation = Location.Create();
            PaymentPerDistance = Multiplier * Company.CompanyList.Last().DefaultPaymentPerDistance;
            CourierStatus = 0;
            direction = 0;
            Company.Curiers.Add(this);
        }
    }



    internal class FootCourier : Couriers
    {
        private double Multiplier = Program.FootCostMultiplier;
        public FootCourier()
        {
            Type = Program.FootType;
            Name = Couriers.GenerateName();
            CarryingCapacity = Couriers.GenerateCapacity();
            Speed = Couriers.GenerateSpeed(Type);
            StartLocation = Location.Create();
            PaymentPerDistance = Multiplier * Company.CompanyList.Last().DefaultPaymentPerDistance;
            CourierStatus = 0;
            direction = 0;
            Company.Curiers.Add(this);
        }
    }
}
