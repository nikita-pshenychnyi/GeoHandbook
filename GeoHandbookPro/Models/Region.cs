using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeoHandbookPro.Models
{
    /// <summary>
    /// Представляє регіон (область, штат тощо) всередині країни.
    /// </summary>
    public class Region
    {
        /// <summary>
        /// Унікальний ідентифікатор регіону (Первинний ключ).
        /// </summary>
        public long RegionID { get; set; }

        /// <summary>
        /// Назва регіону.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип регіону (наприклад, "область", "штат", "провінція").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Чисельність населення регіону. Може бути NULL.
        /// </summary>
        public long? Population { get; set; }

        /// <summary>
        /// Назва столиці регіону.
        /// </summary>
        public string CapitalCityName { get; set; }

        /// <summary>
        /// Ідентифікатор країни, до якої належить регіон (Зовнішній ключ).
        /// </summary>
        public long CountryID { get; set; }

        // Навігаційна властивість (опціонально)
        // public virtual Country ParentCountry { get; set; }

        /// <summary>
        /// Повертає рядкове представлення об'єкта Region (назву регіону).
        /// </summary>
        /// <returns>Назва регіону.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
