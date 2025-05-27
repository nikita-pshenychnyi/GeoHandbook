using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoHandbookPro.Models
{
    /// <summary>
    /// Представляє місто.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Унікальний ідентифікатор міста (Первинний ключ).
        /// </summary>
        public long CityID { get; set; }

        /// <summary>
        /// Назва міста.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Географічна широта міста.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Географічна довгота міста.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Чисельність населення міста. Може бути NULL.
        /// </summary>
        public long? Population { get; set; }

        /// <summary>
        /// Ідентифікатор регіону, до якого належить місто (Зовнішній ключ). Може бути NULL.
        /// </summary>
        public long? RegionID { get; set; }

        /// <summary>
        /// Ідентифікатор країни, до якої належить місто (Зовнішній ключ).
        /// </summary>
        public long CountryID { get; set; }

        // Навігаційні властивості (опціонально)
        // public virtual Region ParentRegion { get; set; }
        // public virtual Country ParentCountry { get; set; }

        /// <summary>
        /// Повертає рядкове представлення об'єкта City (назву міста).
        /// </summary>
        /// <returns>Назва міста.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
