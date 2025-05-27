using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeoHandbookPro.Models
{
    /// <summary>
    /// Представляє країну.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Унікальний ідентифікатор країни (Первинний ключ).
        /// </summary>
        public long CountryID { get; set; }

        /// <summary>
        /// Назва країни.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Площа країни в квадратних кілометрах. Може бути NULL.
        /// </summary>
        public double? Area { get; set; }

        /// <summary>
        /// Чисельність населення країни. Може бути NULL.
        /// </summary>
        public long? Population { get; set; }

        /// <summary>
        /// Форма державного правління.
        /// </summary>
        public string GovernmentForm { get; set; }

        /// <summary>
        /// Назва столиці країни.
        /// </summary>
        public string CapitalCityName { get; set; }

        /// <summary>
        /// Ідентифікатор материка, до якого належить країна (Зовнішній ключ).
        /// </summary>
        public long ContinentID { get; set; }

        // Якщо ви плануєте використовувати ORM або для зручності,
        // можна додати навігаційну властивість до материка:
        // public virtual Continent ParentContinent { get; set; }

        /// <summary>
        /// Повертає рядкове представлення об'єкта Country (назву країни).
        /// </summary>
        /// <returns>Назва країни.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
