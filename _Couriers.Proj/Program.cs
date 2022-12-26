using System;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;


namespace _Couriers.Proj
{
    internal class Program
    {
        // УПРАВЛЕНИЕ КОНСОЛЬЮ И ПОКАЗОМ ЛОГО
        const int STD_OUTPUT_HANDLE = -11;
        [DllImport("kernel32.dll")]
        static extern IntPtr GetStdHandle(int handle);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleDisplayMode(IntPtr ConsoleHandle, uint Flags, IntPtr NewScreenBufferDimensions);
        public static bool LogoThreadCondition = true;
        public static int Mode = -1;
        public static bool AllOrdersDeterminated = false;
        static bool ShowLogo = true; // Показывать ли лого


        // ПАРАМЕТРЫ РАБОТЫ ПРОГРАММЫ
        public static int Field = 15; // Размер поля в км
        public static string companyName = " ©\"Turbo Delivery\"";
        public static string currency = " руб";
        public static double defaultPaymentPerDistance = 70;
        /// <summary>
        /// Политика компании. Если true, компания принимает все заказы, вне зависимости от того, принесут они прибыль или нет
        /// </summary>
        public static bool LoyalPolicy = false;
        /// <summary>
        /// "Прибавочная стоимость" компании. Устанавлевает минимальный процент чистой прибыли компании (от общей суммы заказчика) 
        /// </summary>
        public static double SurplusValue = 0.2; //* 100%

        public static double AverageCustomerPayment = 1000; // Средняя сумма денег, которую готов заплатить клиент за заказ
        public static int AverageOrderTime = 4;  // Среднее время, за которое нужно выполнить заказ
        public static int AverageWeight = 6; // Средний вес заказа
        public static double OrderTimeMultiplier = 0.2;

        public static int CouriersCount = 4;
        public static int OrdersCount = 7;
        public static int MobileCourrierSpeed = 10; // Средняя скорость курьеров в км/ч
        public static int FootCourrierSpeed = 10;
        public static double SpeedMultiplier = 0.4;
        public static int AverageCapacity = 5; // Средняя грузоподъёмность

        public static string MobileType = "(На самокате)";
        public static string FootType = "(Пешеход)";
        public static double MobileCostMultiplier = 1.1;  //Множитель наценки курьера (доп. оплата мобильным курьерам)
        public static double FootCostMultiplier = 1.0;

        public static int ShiftDurationPerVirtualHours = 8; // Продолжительность "смены" компании в часах системного времени
        public static int TimeAcceleration = 1000; // Ускорение времени (Во сколько раз системное время быстрее реального)
        public static bool WakeUp = false;

        static void Main(string[] args)
        {
            if (ShowLogo) // Заставка
            {
                Console.SetWindowSize(Logo.SizeX, Logo.SizeY);
                Console.SetBufferSize(Logo.SizeX, Logo.SizeY);
                var hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
                SetConsoleDisplayMode(hConsole, 1, IntPtr.Zero);
                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

                Thread thread1 = new Thread(Logo.Escape); // Инициализация потока для закрытия заставки при помощи клавиши "Пробел"
                thread1.IsBackground = true;
                thread1.Start();
                Logo.PrintLogo();
                if (LogoThreadCondition == false) thread1.Abort();
            }
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(Logo.SizeX - 20, Logo.SizeY);
            Console.CursorVisible = false;

            ViewEvent.ChooseTheMode();

            if (Mode == 0)
            {
                ViewEvent.IntrodutionDemo();
                ViewEvent.DemoData();

                Methods.CreateCompany(companyName, defaultPaymentPerDistance, Field, LoyalPolicy);
                Methods.PrintCurrentCompany();
                Time StartTime = new Time();
                Methods.AddOrder(OrdersCount);
                Methods.AddCourier(CouriersCount);

                Methods.PrintOrders();
                Methods.PrintCouriers();

                Thread thread4 = new Thread(Time.Run);
                Thread thread5 = new Thread(Methods.CurrrentInfornation);
                Thread thread6 = new Thread(Methods.Escape);
                thread4.IsBackground = true;
                thread5.IsBackground = false;
                thread6.IsBackground = true;
                thread4.Start();
                thread5.Start();
                Thread.Sleep(10);
                thread6.Start();
                Planning.TotalDetermination();
            }
            if (Mode == 1)
            {
                ViewEvent.DevOptions();
                Methods.CreateCompany(companyName, defaultPaymentPerDistance, Field, LoyalPolicy);
                Methods.PrintCurrentCompany();
                Time StartTime = new Time();
                Methods.AddOrder(OrdersCount);
                Methods.AddCourier(CouriersCount);

                Methods.PrintOrders();
                Methods.PrintCouriers();

                Thread thread4 = new Thread(Time.Run);
                Thread thread5 = new Thread(Methods.CurrrentInfornation);
                Thread thread6 = new Thread(Methods.Escape);
                thread4.IsBackground = true;
                thread5.IsBackground = false;
                thread6.IsBackground = true;
                thread4.Start();
                thread5.Start();
                Thread.Sleep(10);
                thread6.Start();
                Planning.TotalDetermination();
            }
        }
    }
}
