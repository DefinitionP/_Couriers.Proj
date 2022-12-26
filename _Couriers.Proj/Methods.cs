using System;
using System.Linq;
using System.Threading;



namespace _Couriers.Proj
{
    public class Methods
    {
        public static void AddOrder(int count)
        {
            Random type = new Random();
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(10);
                int q = type.Next(0, 2);
                if (q == 0)
                {
                    Order order = new Delivery();
                    continue;
                }
                else
                {
                    Order order = new Pickup();
                    continue;
                }
            }
        }
        public static void AddCourier(int count)
        {
            Random type = new Random();
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(10);
                int q = type.Next(0, 2);
                if (q == 0)
                {
                    Couriers courier = new MobileCourier();
                    continue;
                }
                else
                {
                    Couriers courier = new FootCourier();
                    continue;
                }
            }
        }
        public static void CreateCompany(string companyName, double defaultPaymentPerDistance, int Field, bool loyalPolicy)
        {
            Company CurrentCompany = new Company(companyName, defaultPaymentPerDistance, Field, loyalPolicy);
        }
        public static void PrintCurrentCompany()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Компания " + Company.CompanyList.Last().CompamyName);
            Console.Write("   Прирост капитала: ");
            ViewEvent.DarkYellow(Math.Round(Company.CompanyList.Last().Profit, 1).ToString() + Company.CompanyList.Last().Currency + '\n');
            Console.Write("Стандартная выплата курьеру за километр: ");
            Console.Write(Company.CompanyList.Last().DefaultPaymentPerDistance + "\n");
            Console.Write("Область доставки заказов: ");
            Console.Write(Company.CompanyList.Last().FieldSize + "x" + Company.CompanyList.Last().FieldSize + "\n");
            if (Company.CompanyList.Last().LoyalPolicy == true)
            {
                Console.Write("Политика компании: ");
                Console.Write("Компания принимает заказ, даже если это принесёт убытки\n\n");
            }
            if (Company.CompanyList.Last().LoyalPolicy == false)
            {
                Console.Write("Политика компании: ");
                Console.Write("Если выполнение заказа принесёт компании убыток, заказ отменяется\n\n");
            }
        }
        public static void PrintOrders()
        {
            Console.SetCursorPosition(0, 5);
            int count = 0;
            Console.WriteLine("Всего заказов: " + Order.OrderQueue.Count);
            Console.Write("Номер"); Console.SetCursorPosition(7, 6);
            Console.Write("Координаты"); Console.SetCursorPosition(25, 6);
            Console.Write("Цена"); Console.SetCursorPosition(37, 6);
            Console.Write("Тип"); Console.SetCursorPosition(48, 6);
            Console.Write("Вес"); Console.SetCursorPosition(54, 6);
            Console.Write("Ост. время"); Console.SetCursorPosition(70, 6);
            Console.Write("Ост. дистанция курьера"); Console.SetCursorPosition(94, 6);
            Console.Write("Статус заказа");
            for (int i = 0; i < Order.OrderQueue.Count; i++)
            {
                var order = Order.OrderQueue[i];
                Console.SetCursorPosition(0, 7 + count);
                Console.Write(" " + (count + 1)); Console.SetCursorPosition(7, 7 + count);
                Console.Write(order.From.ToString() + " " + order.To.ToString());
                Console.SetCursorPosition(25, 7 + count);
                Console.Write(order.CustomerPayment + Program.currency);
                Console.SetCursorPosition(37, 7 + count);
                if (order.IsDelivery == true) Console.Write("Доставка");
                if (order.IsDelivery == false) Console.Write("Забор");
                Console.SetCursorPosition(48, 7 + count);
                Console.Write(order.Weight);
                Console.SetCursorPosition(54, 7 + count);
                if (order.OrderTime == new TimeSpan(0, 0, 0)) Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Осталось " + "{0:D2}:{1:D2}", order.OrderTime.Hours, order.OrderTime.Minutes);
                Console.ResetColor();
                Console.SetCursorPosition(70, 7 + count);
                if (order.DistanceLeft == "завершено") Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(order.DistanceLeft);
                Console.ResetColor();
                Console.SetCursorPosition(94, 7 + count);
                if (order.Status == -1) ViewEvent.Gray("В очереди обработки");
                if (order.Status == 0) ViewEvent.DarkGray("Слишком тяжёлый");
                if (order.Status == 1) ViewEvent.DarkGray("Курьеры не успеют");
                if (order.Status == 2) ViewEvent.DarkGray("Не выгоден компании");
                if (order.Status == 3) ViewEvent.Gray("Обработан");
                if (order.Status == 4) ViewEvent.DarkCyan("Курьер выехал");
                if (order.Status == 5) ViewEvent.DarkCyan("Курьер забрал груз");
                if (order.Status == 6) ViewEvent.DarkGreen("Заказ выполнен");
                if (order.Status == 7) ViewEvent.Red("Заказ просрочен");
                count++;
            }
            Console.WriteLine();
        }
        public static void PrintCouriers()
        {
            int count = 1;
            Console.SetCursorPosition(0, (Order.OrderQueue.Count + 8));
            Console.WriteLine("Всего активных курьеров: " + Company.Curiers.Count);
            Console.Write("Номер"); Console.CursorLeft = 7;
            Console.Write("Местоположение"); Console.CursorLeft = 25;
            Console.Write("Скорость"); Console.CursorLeft = 39;
            Console.Write("Имя"); Console.CursorLeft = 53;
            Console.Write("Тип"); Console.CursorLeft = 69;
            Console.Write("Оплата за км"); Console.CursorLeft = 83;
            Console.Write("Грузоподъёмность"); Console.CursorLeft = 101;
            Console.WriteLine("Статус");
            for (int i = 0; i < Company.Curiers.Count(); i++)
            {
                var currier = Company.Curiers[i];
                Console.SetCursorPosition(1, (Order.OrderQueue.Count + 9 + count));
                Console.Write((Company.Curiers.IndexOf(currier) + 1)); Console.SetCursorPosition(8, (Order.OrderQueue.Count + 9 + count));
                Console.Write(currier.StartLocation.ToString()); Console.SetCursorPosition(25, (Order.OrderQueue.Count + 9 + count));
                Console.Write((currier.Speed.ToString()) + " км/ч"); Console.SetCursorPosition(39, (Order.OrderQueue.Count + 9 + count));
                Console.Write(currier.Name); Console.SetCursorPosition(53, (Order.OrderQueue.Count + 9 + count));
                Console.Write(currier.Type); Console.SetCursorPosition(69, (Order.OrderQueue.Count + 9 + count));
                Console.Write(currier.PaymentPerDistance + Program.currency); Console.SetCursorPosition(83, (Order.OrderQueue.Count + 9 + count));
                Console.Write(currier.CarryingCapacity); Console.SetCursorPosition(101, (Order.OrderQueue.Count + 9 + count));
                if (currier.CourierStatus == 0) ViewEvent.Green("Свободен");
                if (currier.CourierStatus == 1)
                {
                    ViewEvent.DarkCyan("Выполняет заказ " + (Order.OrderQueue.IndexOf(currier.CourierPlane.First()) + 1));
                    if (currier.direction == 1) ViewEvent.DarkCyan(", на пути к грузу");
                    if (currier.direction == 2) ViewEvent.DarkCyan(", забрал груз");
                }
                count++;
            }
        }
        public static void CurrrentInfornation()
        {
            int sleep = 400;
            for (; ; )
            {
                Console.SetCursorPosition(0, 0);
                ReCountOrderTime();
                Planning.ReCountStatus();
                PrintString(4);
                PrintCurrentCompany();
                PrintString(Order.OrderQueue.Count + 3);
                PrintOrders();
                PrintString(Company.Curiers.Count + 3);
                PrintCouriers();

                int cursorY = Console.CursorTop;
                Console.SetCursorPosition(Logo.SizeX - 50, 0);
                Console.Write("                ");
                Console.SetCursorPosition(Logo.SizeX - 50, 0);
                Console.Write(Time.timelist.Last().time.ToString("g"));
                if (Program.WakeUp) break;
                Console.CursorTop = cursorY + 3;
                Console.CursorLeft = 0;
                ViewEvent.VievLog(5);
                Thread.Sleep(sleep);
            }
            Console.SetCursorPosition(Logo.SizeX - 50, 1);
            ViewEvent.DarkGreen("Смена длилась " + (Time.timelist.Last().time - Time.timelist[0].time).ToString(@"hh\:mm\:ss"));
            ViewEvent.Exit();
        }
        public static void ReCountOrderTime()
        {
            if (Time.TimeCounters.Count != 0)
            {
                TimeSpan CurrentDelta = Time.timelist.Last().time - Time.TimeCounters.First().time;
                foreach (var order in Order.OrderQueue)
                {
                    order.OrderTime = order.OrderTime - CurrentDelta;
                    if (order.OrderTime.TotalMinutes <= 0)
                    {
                        order.OrderTime = new TimeSpan(0, 0, 0);
                    }
                    if (order.SelectrdCourier != null)
                    {
                        if ((Planning.IsDeterminated(order) == true) && (order.SelectrdCourier.CourierStatus == 1))
                        {
                            TimeSpan delta = Time.timelist.Last().time - order.StartOrderTime;
                            double distLeft = Math.Round(Planning.FullOrderDistance(order.SelectrdCourier, order) - delta.TotalHours *
                                order.SelectrdCourier.Speed);
                            if (distLeft <= 0) distLeft = 0;
                            order.DistanceLeft = (distLeft + " км");
                        }
                        if (order.Status == 6) order.DistanceLeft = "завершено";
                        if (order.Status == 7) order.DistanceLeft = "не выполнено";
                    }
                }
            }
            Time count = new Time(1);
        }
        public static void PrintString(int count)
        {
            int cursor = Console.CursorTop;
            Console.CursorLeft = 0;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("                                                                                                                                                  ");
            }
            Console.CursorTop = cursor;
        }

        public static void Escape()
        {
            ConsoleKeyInfo Selected;
            Selected = Console.ReadKey(true);
            while (Program.WakeUp == false)
            {
                if (Selected.Key == ConsoleKey.C)
                {
                    AddCourier(1);
                    ViewEvent view = new ViewEvent.LogEvents(4);
                    Planning.Distribution();

                }
                if (Selected.Key == ConsoleKey.O)
                {
                    AddOrder(1);
                    ViewEvent view = new ViewEvent.LogEvents(5);
                    Planning.Distribution();
                }
                if (Selected.Key == ConsoleKey.Escape)
                {
                    Program.WakeUp = true; 
                }

                Selected = Console.ReadKey(true);
            }
        }
    }
}
