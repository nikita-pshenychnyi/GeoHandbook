using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoHandbookPro.Models
{
    /// <summary>
    /// Представляє географічний материк.
    /// </summary>
    public class Continent
    {
        /// <summary>
        /// Унікальний ідентифікатор материка (Первинний ключ).
        /// </summary>
        public long ContinentID { get; set; }

        /// <summary>
        /// Назва материка.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Повертає рядкове представлення об'єкта Continent (назву материка).
        /// Це корисно для відображення в ComboBox.
        /// </summary>
        /// <returns>Назва материка.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
