using System;
using System.Data;
using System.Linq;
using System.Threading;


namespace _Couriers.Proj
{
    /// <summary>
    /// Класс со статическими методами, использующимися для планирования
    /// </summary>
    public class Planning
    {
        /// <summary>
        /// Главный метод распределения. Действует в отдельном потоке
        /// </summary>
        public static void TotalDetermination()
        {
            int ViewCheck = -1;
            for (; ; )
            {
                int i;
                do
                {
                    i = 0;
                    foreach (var order in Order.OrderQueue)
                    {
                        if (IsDeterminated(order) == false) i++;
                    }
                    Distribution();
                    ChooseTheBest();
                    Thread.Sleep(200);
                }
                while (i != 0);

                if (ViewCheck != Order.OrderQueue.Count())
                {
                    ViewEvent view = new ViewEvent.LogEvents(1);
                }
                ViewCheck = Order.OrderQueue.Count();
            }
        }
        /// <summary>
        /// Перебор всех возможных комбинаций "курьер-заказ"
        /// </summary>
        public static void Distribution()
        {
            UnsortedOffers.OrdersToSort.Clear();
            for (int i = 0; i < Order.OrderQueue.Count; i++)
            {
                var order = Order.OrderQueue[i];
                if (IsDeterminated(order)) continue;
                if (order.Status == 7) continue;
                order.Status = -1;
                int cantCatchCount = 0;
                for (int e = 0; e < Company.Curiers.Count(); e++)
                {
                    var curier = Company.Curiers[e];
                    if (CanCatch(curier, order))
                    {
                        if (curier.CourierStatus == 1)
                        {
                            continue;
                        }
                        if (curier.CourierStatus == 0)
                        {
                            if (WillCatchInTime(curier, order))
                            {
                                CheckOrderProfit(curier, order);
                                if ((Company.CompanyList.Last().LoyalPolicy == false) && (order.IsProfitable == false))
                                {
                                    order.Status = 2;
                                    continue;
                                }
                                UnsortedOffers offer = new UnsortedOffers(GetProfit(curier, order), curier, order);
                                order.Status = 3;
                            }
                            else order.Status = 1;
                        }

                    }
                    else cantCatchCount++;
                    if (cantCatchCount == Company.Curiers.Count()) order.Status = 0;
                }
            }
        }
        /// <summary>
        /// Выбор самой выгодной для компании комбинации "курьер-заказ"
        /// </summary>
        private static void ChooseTheBest()
        {
            while (UnsortedOffers.OrdersToSort.Count() != 0)
            {
                var sortedOffers = UnsortedOffers.OrdersToSort.OrderByDescending(x => x.Profit);
                var theBest = sortedOffers.First();
                UnsortedOffers.SortedOrders.Add(theBest);
                var Sorted = sortedOffers.ToList();
                int qnt = Sorted.Count();
                for (int i = 0; i < qnt; i++)
                {

                    var unsort = Sorted[i];
                    if ((unsort.order == theBest.order) | (unsort.courier == theBest.courier))
                    {
                        UnsortedOffers.OrdersToSort.Remove(unsort);
                    }
                }

                // После выбора лучшего варианта "курьер-заказ" все остальные варианты с совпадающим параметром "курьер" или "заказ"
                // удаляются из списка
                int o = Order.OrderQueue.IndexOf(theBest.order);
                Order.OrderQueue[o].Status = 4;
                // Изменение состояния заказа - курьер выехал.
                Order.OrderQueue[o].RequiredTime = GetRequiredTime(theBest.courier, theBest.order);
                // Присвоение полю "требуемое время" значения времени, требуемого для выполнения заказа выбранным курьером
                int q = Company.Curiers.IndexOf(theBest.courier);
                Company.Curiers[q].CourierPlane.Add(theBest.order);
                // Добавление заказа в расписание курьера.
                Company.Curiers[q].CourierStatus = 1;
                // Изменение статуса курьера.
                Company.Curiers[q].direction = 1;
                //Console.WriteLine(theBest.Profit);
                Company.Curiers[q].CourierPlane.Last().StartOrderTime = Time.timelist.Last().time;
                theBest.order.SelectrdCourier = theBest.courier;
                theBest.order.FinalProfit = GetProfit(theBest.courier, theBest.order);
                ViewEvent view = new ViewEvent.LogEvents(theBest);
            }
        }
        public static void ReCountStatus()
        {
            foreach (var curier in Company.Curiers)
            {
                if (curier.CourierStatus == 1)
                {
                    if (curier.CourierPlane.First().IsDelivery == true)
                    {
                        TimeSpan Required = TimeSpan.FromSeconds(curier.CourierPlane.First().RequiredTime);
                        TimeSpan CurrentDelta = Time.timelist.Last().time - curier.CourierPlane.First().StartOrderTime;
                        if (CurrentDelta >= Required)
                        {
                            curier.CourierStatus = 0;
                            curier.direction = 0;
                            curier.CourierPlane.First().Status = 6;
                            CountTheMoney(curier, curier.CourierPlane.Last());
                            curier.StartLocation = curier.CourierPlane.First().To;
                            curier.CourierPlane.Remove(curier.CourierPlane.First());
                            continue;
                        }
                        double timeToGet = DistanceTo(curier.StartLocation, curier.CourierPlane.First().From) / curier.Speed * 3600;
                        TimeSpan DeltaGet = TimeSpan.FromSeconds(timeToGet);
                        if (CurrentDelta < DeltaGet)
                        {
                            curier.direction = 1;
                            curier.CourierPlane.First().Status = 4;
                            continue;
                        }
                        curier.direction = 2;


                        curier.CourierPlane.First().Status = 5;
                    }
                    else
                    {
                        TimeSpan CurrentDelta = Time.timelist.Last().time - curier.CourierPlane.First().StartOrderTime;
                        double time = Math.Ceiling((DistanceTo(curier.StartLocation, curier.CourierPlane.First().From) +
                            DistanceTo(curier.CourierPlane.First().From, curier.CourierPlane.First().To)) / curier.Speed * 3600);
                        TimeSpan fullDelta = TimeSpan.FromSeconds(time);
                        if (CurrentDelta >= fullDelta)
                        {
                            curier.CourierStatus = 0;
                            curier.direction = 0;
                            curier.CourierPlane.First().Status = 6;
                            CountTheMoney(curier, curier.CourierPlane.Last());
                            curier.StartLocation = curier.CourierPlane.First().To;
                            curier.CourierPlane.Remove(curier.CourierPlane.First());
                            continue;
                        }
                        TimeSpan Required = TimeSpan.FromSeconds(curier.CourierPlane.First().RequiredTime);
                        if (CurrentDelta >= Required)
                        {
                            curier.CourierStatus = 1;
                            curier.direction = 2;
                            curier.CourierPlane.First().TimeCheckPoint = Time.timelist.Last().time;
                            curier.CourierPlane.First().Status = 5;
                            continue;
                        }
                        curier.CourierStatus = 1;
                        curier.direction = 1;
                        curier.CourierPlane.First().Status = 4;
                        continue;
                    }
                }
            }
            int count = 0, count2 = 0;
            foreach (var order in Order.OrderQueue)
            {
                if ((order.OrderTime == new TimeSpan(0, 0, 0)) && (IsDeterminated(order) == false))
                {
                    order.Status = 7;
                }
                if (order.Status == 6)
                {
                    order.OrderTime = new TimeSpan(0, 0, 0);
                    count++;
                }
                if ((IsDeterminated(order) == false) && order.Status != (-1)) count2++;

                bool OrdersIsOver = false;
                if (count == Order.OrderQueue.Count)
                {
                    if (Order.OrderQueue.Count == ViewEvent.StatusHelper) return;
                    ViewEvent view = new ViewEvent.LogEvents(2);
                    ViewEvent.StatusHelper = Order.OrderQueue.Count;
                    OrdersIsOver = true;
                }
                if ((count + count2) == Order.OrderQueue.Count)
                {
                    if (OrdersIsOver == true) return;
                    if (Order.OrderQueue.Count == ViewEvent.StatusHelper2) return;
                    ViewEvent view = new ViewEvent.LogEvents(3);
                    ViewEvent.StatusHelper2 = Order.OrderQueue.Count;
                }
            }
        }
        private static double DistanceTo(Location from, Location to)
        {
            double distance;
            distance = Math.Sqrt(Math.Pow(Math.Abs(from.CoordX - to.CoordX), 2)
                + Math.Pow(Math.Abs(from.CoordY - to.CoordY), 2));
            return distance;
        }
        public static double FullOrderDistance(Couriers curier, Order order)
        {
            double distance = DistanceTo(curier.StartLocation, order.From) + DistanceTo(order.From, order.To);
            return distance;
        }
        /// <summary>
        /// Проверка грузоподъёмности курьера
        /// </summary>
        /// <param name="curier"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private static bool CanCatch(Couriers curier, Order order)
        {
            if (curier.CarryingCapacity >= order.Weight) return true; else return false;
        }
        /// <summary>
        /// Вычисление времени, требуемого для выполнения заказа курьером
        /// </summary>
        /// <param name="curier"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private static double GetRequiredTime(Couriers curier, Order order)
        {
            double time = 0;
            if (order.IsDelivery == true)
            {
                time = Math.Ceiling((DistanceTo(curier.StartLocation, order.From) + // Округление интервала времени в наибольшую сторону!
                    DistanceTo(order.From, order.To)) / curier.Speed * 3600); // Время в сек - для избежания серьёзных погрешностей при округлении

            }
            if (order.IsDelivery == false)
            {
                time = Math.Ceiling(DistanceTo(curier.StartLocation, order.From) / curier.Speed * 3600);
            }
            return time;
        }
        /// <summary>
        /// Проверка, успеет ли курьер выполнить заказ вовремя
        /// </summary>
        /// <param name="curier"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private static bool WillCatchInTime(Couriers curier, Order order) // Успеет ли курьер выполнить заказ вовремя
        {
            TimeSpan delta = TimeSpan.FromSeconds((int)GetRequiredTime(curier, order));
            TimeSpan CurrentDelta = Time.timelist.Last().time - order.TimeCheckPoint;
            if ((delta + CurrentDelta) <= order.OrderTime)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Проверка, приносит ли заказ прибыль (результат зависит от текущей политики компании)
        /// </summary>
        /// <param name="curier"></param>
        /// <param name="order"></param>
        private static void CheckOrderProfit(Couriers curier, Order order)
        {
            double expenses = FullOrderDistance(curier, order) * curier.PaymentPerDistance;
            if ((order.CustomerPayment - expenses) > order.CustomerPayment * Program.SurplusValue) order.IsProfitable = true;
            else order.IsProfitable = false;
        }
        public static bool IsDeterminated(Order order)
        {
            if (order.Status == 4) return true;
            if (order.Status == 5) return true;
            if (order.Status == 6) return true;
            return false;
        }
        private static void CountTheMoney(Couriers curier, Order order)
        {
            Company.CompanyList.Last().Profit = Company.CompanyList.Last().Profit + GetProfit(curier, order);
        }

        /// <summary>
        /// Метод возвращает прибыль, которую компания получит в результате выполнения заказа
        /// </summary>
        /// <param name="curier"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private static double GetProfit(Couriers curier, Order order)
        {
            double expenses = FullOrderDistance(curier, order) * curier.PaymentPerDistance;
            double profit = order.CustomerPayment - expenses;
            return profit;
        }
    }
}
