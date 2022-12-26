using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _Couriers.Proj
{
    public class ViewEvent
    {
        public static int StatusHelper = -1;
        public static int StatusHelper2 = -1;
        public static List<LogEvents> Log = new List<LogEvents>();
        public DateTime EventTime { get; set; }
        public int EventType { get; set; }
        public int CourierInd { get; set; }
        public int OrderInd { get; set; }
        public double Profit { get; set; }
        public string CourierName { get; set; }
        public string LogInfo { get; set; }

        public static void VievLog(int logsize)
        {
            Console.CursorTop = Company.Curiers.Count + Order.OrderQueue.Count + 11;
            Console.CursorTop--;
            Methods.PrintString(1);
            Console.CursorTop++;
            if (Log.Count == 0) return;
            int i = 0;
            if (Log.Count() > logsize) i = Log.Count() - logsize;
            for (int q = i; q < Log.Count(); q++)
            {
                Console.CursorTop++;
                Methods.PrintString(1);
                Console.CursorTop--;
                if (Log[q].EventType == 1) Console.ForegroundColor = ConsoleColor.DarkCyan;
                if (Log[q].EventType == 2) Console.ForegroundColor = ConsoleColor.DarkGreen;
                if (Log[q].EventType == 3) Console.ForegroundColor = ConsoleColor.DarkRed;
                if (Log[q].EventType == 4) Console.ForegroundColor = ConsoleColor.Magenta;
                if (Log[q].EventType == 5) Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(Log[q].LogInfo);
                Console.ResetColor();
            }
        }

        public static void PingPrint(string str)
        {
            var Arr = Encoding.Default.GetChars(Encoding.Default.GetBytes(str));
            for (int i = 0; i < Arr.Length; i++)
            {
                Thread.Sleep(7);
                DarkGreen(Arr[i].ToString());
            }
        }
        public static void IntrodutionDemo()
        {
            Console.Clear();
            StartCursor();
            DarkGreen("Клавиши управления");
            StartCursor(2);
            PingPrint("Добавление заказа - [O]");
            StartCursor(3);
            PingPrint("Добавление курьера - [C]");
            StartCursor(4);
            PingPrint("Прерывание программы - [Esc]");
            StartCursor(7);
            PingPrint("...Нажмите любую клавишу...");
            Console.ReadKey();
            Thread.Sleep(50);
            Console.Clear();
            StartCursor();
            Console.CursorVisible = true;
            PingPrint("Запуск демонстрационного режима...");
            Thread.Sleep(1000);
            Console.CursorVisible = false;
            Console.Clear();
        }
        public static void StartCursor()
        {
            Console.SetCursorPosition(3, 5);
        }
        public static void StartCursor(int y)
        {
            Console.SetCursorPosition(3, 5 + y);
        }
        public static void ChooseTheMode()
        {
            StartCursor();
            DarkGreen("Выберите рабочий режим:");
            StartCursor(1);
            PingPrint("Для работы программы в демо-режиме с параметрами по умолчанию нажмите D");
            StartCursor(2);
            PingPrint("Для запуска \"режима тестировщика\" нажмите T");
            ConsoleKeyInfo Mode;
            Mode = Console.ReadKey(true);
            while ((Mode.Key != ConsoleKey.D) && (Mode.Key != ConsoleKey.T))
            {
                Console.Clear();
                StartCursor();
                PingPrint("Данные введены неверно");
                Thread.Sleep(500);
                Console.Clear();
                StartCursor();
                PingPrint("Для работы программы в демо-режиме с параметрами по умолчанию нажмите D");
                StartCursor(1);
                PingPrint("Для запуска \"режима тестировщика\" нажмите T");
                Mode = Console.ReadKey(true);

            }
            if (Mode.Key == ConsoleKey.D) Program.Mode = 0;
            if (Mode.Key == ConsoleKey.T) Program.Mode = 1;

        }
        public static void DevOptions()
        {
            ChooseOptions();
            Console.Clear();
            StartCursor();
            DarkGreen("Клавиши управления");
            StartCursor(2);
            PingPrint("Добавление заказа - [O]");
            StartCursor(3);
            PingPrint("Добавление курьера - [C]");
            StartCursor(4);
            PingPrint("Прерывание программы - [Esc]");
            StartCursor(7);
            PingPrint("...Нажмите любую клавишу...");
            Console.Clear();
            Console.SetCursorPosition(3, 5);
            Console.CursorVisible = true;
            PingPrint("Запуск \"режима тестировщика\"...");
            Thread.Sleep(1000);
            Console.CursorVisible = false;
            Console.Clear();
        }


        public static void DemoData()
        {
            int x, y;
            StartCursor();
            PingPrint("Введите число курьеров: ");
            string couriers;
            couriers = Console.ReadLine();

            StartCursor(1);
            PingPrint("Введите число заказов: ");
            string orders;
            orders = Console.ReadLine();

            while (((!int.TryParse(couriers, out x)) | (!int.TryParse(orders, out y))) | ((x <= 0) | (y <= 0)))
            {
                Console.Clear();
                StartCursor();
                PingPrint("Данные введены неверно");
                Thread.Sleep(500);
                Console.Clear();

                StartCursor();
                PingPrint("Введите число курьеров: ");
                couriers = Console.ReadLine();

                StartCursor(1);
                PingPrint("Введите число заказов: ");
                orders = Console.ReadLine();
            }
            Program.CouriersCount = x;
            Program.OrdersCount = y;
            Console.Clear();
        }
        public static void Exit ()
        {
            Thread.Sleep(500);
            Console.SetCursorPosition(3, Logo.SizeY - 3);
            PingPrint("Программа завершила работу");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(3, Logo.SizeY - 2);
            Environment.Exit(0);
        }
        public static void Red(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(str);
            Console.ResetColor();
        }
        public static void DarkGray(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(str);
            Console.ResetColor();
        }
        public static void Gray(string str)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(str);
            Console.ResetColor();
        }
        public static void DarkCyan(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(str);
            Console.ResetColor();
        }
        public static void DarkGreen(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(str);
            Console.ResetColor();
        }
        public static void Green(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(str);
            Console.ResetColor();
        }
        public static void DarkYellow(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(str);
            Console.ResetColor();
        }
        public class LogEvents : ViewEvent
        {
            public LogEvents(UnsortedOffers offer)
            {
                EventTime = Time.timelist.Last().time;
                CourierInd = Company.Curiers.IndexOf(offer.courier) + 1;
                OrderInd = Order.OrderQueue.IndexOf(offer.order) + 1;
                CourierName = offer.courier.Name;
                Profit = Math.Round(offer.order.FinalProfit, 1);
                LogInfo = EventTime.ToString("t") + "  Курьер " + CourierName + " (" + CourierInd + ") взял заказ (" + OrderInd + "), прибыль " + Profit;
                Log.Add(this);
                EventType = 0;

            }
            public LogEvents(int q)
            {
                EventTime = Time.timelist.Last().time;
                EventType = q;
                if (EventType == 1) LogInfo = EventTime.ToString("t") + "  Все заказы распределены                                ";
                if (EventType == 2) LogInfo = EventTime.ToString("t") + "  Все заказы выполнены                                   ";
                if (EventType == 3) LogInfo = EventTime.ToString("t") + "  Нет подходящих заказов                                 ";
                if (EventType == 4)
                {
                    LogInfo = EventTime.ToString("t") + "  Добавлен курьер " + Company.Curiers.Last().Name + " (" +
                        Company.Curiers.Count() + ")";
                }
                if (EventType == 5)
                {
                    LogInfo = EventTime.ToString("t") + "  Поступил новый заказ, клиент заплатит " + Order.OrderQueue.Last().CustomerPayment;
                }
                Log.Add(this);
            }
        }

        public static void ChooseOptions ()
        {
            int Cost, Salary, Field, Shift, OrderTime, Time, Speed, Speed2, Capacity, Weight;

            Console.Clear();
            StartCursor();
            DarkGreen("\"Режим тестировщика\" позволяет задавать параметры работы программы");

        Enter0:
            StartCursor(2);
            PingPrint("Введите размер поля: ");
            string field = Console.ReadLine();
            if (!(int.TryParse(field, out Field) && Field > 0))
            {
                StartCursor(2);
                Methods.PrintString(1); StartCursor(2);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter0;
            }
            Program.Field = Field;
        Enter1:
            StartCursor(3);
            PingPrint("Введите среднюю стоимость заказа: ");
            string cost = Console.ReadLine();
            if (!(int.TryParse(cost, out Cost) && Cost >= 0))
            {
                StartCursor(3);
                Methods.PrintString(1); StartCursor(3);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter1;
            }
            Program.AverageCustomerPayment = Cost;
        Enter2:
            StartCursor(4);
            PingPrint("Введите среднюю сумму выплаты курьеру за километр:");
            string salary = Console.ReadLine();
            if (!(int.TryParse(salary, out Salary) && Salary >= 0 && Salary <= Cost))
            {
                StartCursor(4);
                Methods.PrintString(1); StartCursor(4);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter2;
            }
            Program.defaultPaymentPerDistance = Salary;
        Enter3:
            StartCursor(5);
            PingPrint("Введите длительность смены в часах: ");
            string shift = Console.ReadLine();
            if (!(int.TryParse(shift, out Shift) && Shift > 0))
            {
                StartCursor(5);
                Methods.PrintString(1); StartCursor(5);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter3;
            }
            Program.ShiftDurationPerVirtualHours = Shift;
        Enter4:
            StartCursor(6);
            PingPrint("Введите среднее время выполнения заказа в часах: ");
            string orderTime = Console.ReadLine();
            if (!(int.TryParse(orderTime, out OrderTime) && OrderTime > 0 && OrderTime <= Shift))
            {
                StartCursor(6);
                Methods.PrintString(1); StartCursor(6);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter4;
            }
            Program.AverageOrderTime = OrderTime;
        Enter5:
            StartCursor(7);
            PingPrint("Введите коэффициент ускорения времени: ");
            string time = Console.ReadLine();
            if (!(int.TryParse(time, out Time) && Time > 0))
            {
                StartCursor(7);
                Methods.PrintString(1); StartCursor(7);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter5;
            }
            Program.TimeAcceleration = Time;
        Enter6:
            StartCursor(8);
            PingPrint("Среднюю скорость пеших курьеров в км/ч: ");
            string speed = Console.ReadLine();
            if (!(int.TryParse(speed, out Speed) && Speed > 0))
            {
                StartCursor(8);
                Methods.PrintString(1); StartCursor(8);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter6;
            }
            Program.FootCourrierSpeed = Speed;
        Enter7:
            StartCursor(9);
            PingPrint("Среднюю скорость мобильных курьеров в км/ч: ");
            string speed2 = Console.ReadLine();
            if (!(int.TryParse(speed2, out Speed2) && Speed2 > 0))
            {
                StartCursor(9);
                Methods.PrintString(1); StartCursor(9);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter7;
            }
            Program.MobileCourrierSpeed = Speed2;
        Enter8:
            StartCursor(10);
            PingPrint("Введите среднюю грузоподъёмность курьеров: ");
            string capacity = Console.ReadLine();
            if (!(int.TryParse(capacity, out Capacity) && Capacity > 0))
            {
                StartCursor(10);
                Methods.PrintString(1); StartCursor(10);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter8;
            }
            Program.AverageCapacity = Capacity;
        Enter9:
            StartCursor(11);
            PingPrint("Введите средний вес груза: ");
            string weight = Console.ReadLine();
            if (!(int.TryParse(weight, out Weight) && Weight > 0))
            {
                StartCursor(11);
                Methods.PrintString(1); StartCursor(11);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter9;
            }
            Program.AverageWeight = Weight;
        Enter10:
            StartCursor(12);
            PingPrint("Выберите политику компании: \n");
            StartCursor(13);
            PingPrint("Если компания должна принимать заказ в любом случае, вне зависимости от прибыли, введите \"Y\"");
            StartCursor(14);
            PingPrint("Если компания должна принимать только те заказы, которые приносят прибыль, введите \"N\"");
            StartCursor(12); Console.CursorLeft = 30;
            string pol = Console.ReadLine();
            if (pol != "N" && pol != "Y" && pol != "y" && pol != "n")
            {
                StartCursor(12);
                Methods.PrintString(3); StartCursor(11);
                PingPrint("Данные введены неверно");
                Thread.Sleep(300);
                Methods.PrintString(1); goto Enter10;
            }
            if (pol == "Y" | pol == "y") Program.LoyalPolicy = true;
            if (pol == "N" | pol == "n") Program.LoyalPolicy = false;

            StartCursor(18);
            PingPrint("...Параметры сохранены...");
            Thread.Sleep(500);
            Console.Clear();

            int x, y;
            StartCursor();
            PingPrint("Введите число курьеров: ");
            string couriers;
            couriers = Console.ReadLine();

            StartCursor(1);
            PingPrint("Введите число заказов: ");
            string orders;
            orders = Console.ReadLine();

            while (((!int.TryParse(couriers, out x)) | (!int.TryParse(orders, out y))) | ((x <= 0) | (y <= 0)))
            {
                Console.Clear();
                StartCursor();
                PingPrint("Данные введены неверно");
                Thread.Sleep(500);
                Console.Clear();

                StartCursor();
                PingPrint("Введите число курьеров: ");
                couriers = Console.ReadLine();

                StartCursor(1);
                PingPrint("Введите число заказов: ");
                orders = Console.ReadLine();
            }
            Program.CouriersCount = x;
            Program.OrdersCount = y;
            Console.Clear();


        }
    }

}
