using System.Collections.Generic;

namespace _Couriers.Proj
{
    public class Company
    {
        /// <summary>
        /// Список компаний. Последний элемент - текущая компания
        /// </summary>
        public static List<Company> CompanyList = new List<Company>();
        /// <summary>
        /// Список курьеров компании
        /// </summary>
        public static List<Couriers> Curiers { get; set; } = new List<Couriers>();
        /// <summary>
        /// Название компании
        /// </summary>
        public string CompamyName { get; set; }
        /// <summary>
        /// Стандартная выплата курьеру за километр пути
        /// </summary>
        public double DefaultPaymentPerDistance { get; set; }
        /// <summary>
        /// Размер поля
        /// </summary>
        public int FieldSize { get; set; }
        /// <summary>
        /// Политика компании. Если true, компания принимает все заказы, вне зависимости от того, принесут они прибыль или нет
        /// </summary>
        public bool LoyalPolicy { get; set; }
        /// <summary>
        /// Суммарная прибыль компании
        /// </summary>
        public double Profit { get; set; }
        /// <summary>
        /// Валюта, использующаяся компанией
        /// </summary>
        public string Currency { get; set; }
        public Company(string companyName, double defaultPaymentPerDistance, int fieldSize, bool loyalPolicy)
        {
            CompamyName = companyName;
            DefaultPaymentPerDistance = defaultPaymentPerDistance;
            FieldSize = fieldSize;
            LoyalPolicy = loyalPolicy;
            Profit = 0;
            Currency = Program.currency;
            CompanyList.Add(this);
        }
    }
}
