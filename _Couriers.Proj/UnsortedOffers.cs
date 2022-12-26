using System.Collections.Generic;


namespace _Couriers.Proj
{
    public class UnsortedOffers
    {
        /// <summary>
        /// Список всех возможных комбинаций "курьер-заказ", удовлетворяющих условиям политики компании
        /// </summary>
        public static List<UnsortedOffers> OrdersToSort = new List<UnsortedOffers>();
        /// <summary>
        /// Список распределённых по курьерам заказов
        /// </summary>
        public static List<UnsortedOffers> SortedOrders = new List<UnsortedOffers>();

        public double Profit { get; set; }
        public Couriers courier { get; set; }
        public Order order { get; set; }

        public UnsortedOffers(double Profit, Couriers courier, Order order)
        {
            this.Profit = Profit;
            this.courier = courier;
            this.order = order;
            OrdersToSort.Add(this);
        }
    }
}
    