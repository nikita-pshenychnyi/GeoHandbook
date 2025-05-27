using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using GeoHandbookPro.Models;
using Region = GeoHandbookPro.Models.Region;

namespace GeoHandbookPro.Services
{
    /// <summary>
    /// Керує всіма операціями з базою даних SQLite.
    /// </summary>
    public class DatabaseManager
    {
        private static readonly string DbFileName = "geography.sqlite";
        private static readonly string ConnectionString = $"Data Source={Path.Combine(Application.StartupPath, DbFileName)};Version=3;";

        /// <summary>
        /// Ініціалізує новий екземпляр класу DatabaseManager та створює базу даних/таблиці, якщо вони не існують.
        /// </summary>
        public DatabaseManager()
        {
            InitializeDatabase();
        }

        /// <summary>
        /// Отримує рядок підключення до бази даних.
        /// </summary>
        /// <returns>Рядок підключення.</returns>
        public static string GetConnectionString()
        {
            return ConnectionString;
        }

        /// <summary>
        /// Ініціалізує базу даних: створює файл БД та таблиці, якщо вони ще не існують.
        /// </summary>
        private void InitializeDatabase()
        {
            string dbFilePath = Path.Combine(Application.StartupPath, DbFileName);
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string createContinentsTableQuery = @"
                CREATE TABLE IF NOT EXISTS Continents (
                    ContinentID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE
                );";

                string createCountriesTableQuery = @"
                CREATE TABLE IF NOT EXISTS Countries (
                    CountryID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE,
                    Area REAL,
                    Population INTEGER,
                    GovernmentForm TEXT,
                    CapitalCityName TEXT,
                    ContinentID INTEGER NOT NULL,
                    FOREIGN KEY (ContinentID) REFERENCES Continents (ContinentID) ON DELETE CASCADE
                );";

                string createRegionsTableQuery = @"
                CREATE TABLE IF NOT EXISTS Regions (
                    RegionID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Type TEXT,
                    Population INTEGER,
                    CapitalCityName TEXT,
                    CountryID INTEGER NOT NULL,
                    FOREIGN KEY (CountryID) REFERENCES Countries (CountryID) ON DELETE CASCADE
                );";

                string createCitiesTableQuery = @"
                CREATE TABLE IF NOT EXISTS Cities (
                    CityID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Latitude REAL NOT NULL,
                    Longitude REAL NOT NULL,
                    Population INTEGER,
                    RegionID INTEGER,
                    CountryID INTEGER NOT NULL,
                    FOREIGN KEY (RegionID) REFERENCES Regions (RegionID) ON DELETE SET NULL,
                    FOREIGN KEY (CountryID) REFERENCES Countries (CountryID) ON DELETE CASCADE
                );";

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = createContinentsTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createCountriesTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createRegionsTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createCitiesTableQuery;
                    command.ExecuteNonQuery();
                }
            }
        }

        #region Continent Operations

        /// <summary>
        /// Отримує всі материки з бази даних.
        /// </summary>
        /// <returns>Список всіх материків.</returns>
        public List<Continent> GetAllContinents()
        {
            var continents = new List<Continent>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT ContinentID, Name FROM Continents ORDER BY Name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            continents.Add(new Continent
                            {
                                ContinentID = Convert.ToInt64(reader["ContinentID"]),
                                Name = reader["Name"].ToString()
                            });
                        }
                    }
                }
            }
            return continents;
        }

        /// <summary>
        /// Додає новий материк до бази даних.
        /// </summary>
        /// <param name="continent">Об'єкт материка для додавання.</param>
        /// <returns>True, якщо додавання успішне, інакше false.</returns>
        public bool AddContinent(Continent continent)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Continents (Name) VALUES (@Name);";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", continent.Name);
                        command.ExecuteNonQuery();
                        continent.ContinentID = connection.LastInsertRowId; // Отримати ID вставленого запису
                        return true;
                    }
                }
            }
            catch (SQLiteException ex) // Обробка помилки унікальності
            {
                // SQLiteErrorCode.Constraint_UNIQUE = 19 (старі версії), або перевірка повідомлення
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    MessageBox.Show($"Помилка: Материк з назвою '{continent.Name}' вже існує.", "Помилка додавання", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Помилка SQLite: {ex.Message}", "Помилка бази даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загальна помилка: {ex.Message}", "Помилка додавання", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Оновлює існуючий материк у базі даних.
        /// </summary>
        /// <param name="continent">Об'єкт материка з оновленими даними.</param>
        /// <returns>True, якщо оновлення успішне, інакше false.</returns>
        public bool UpdateContinent(Continent continent)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "UPDATE Continents SET Name = @Name WHERE ContinentID = @ContinentID;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", continent.Name);
                        command.Parameters.AddWithValue("@ContinentID", continent.ContinentID);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    MessageBox.Show($"Помилка: Материк з назвою '{continent.Name}' вже існує.", "Помилка оновлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Помилка SQLite: {ex.Message}", "Помилка бази даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загальна помилка: {ex.Message}", "Помилка оновлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Видаляє материк з бази даних за його ID.
        /// </summary>
        /// <param name="continentId">ID материка для видалення.</param>
        /// <returns>True, якщо видалення успішне, інакше false.</returns>
        public bool DeleteContinent(long continentId)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    // Перевірка наявності пов'язаних країн
                    string checkQuery = "SELECT COUNT(*) FROM Countries WHERE ContinentID = @ContinentID;";
                    using (var checkCmd = new SQLiteCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@ContinentID", continentId);
                        long countryCount = (long)checkCmd.ExecuteScalar();
                        if (countryCount > 0)
                        {
                            MessageBox.Show($"Неможливо видалити материк, оскільки він містить {countryCount} країн(и). Спочатку видаліть або перепризначте країни.", "Помилка видалення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    string query = "DELETE FROM Continents WHERE ContinentID = @ContinentID;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ContinentID", continentId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка видалення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Country Operations

        /// <summary>
        /// Отримує всі країни з бази даних.
        /// </summary>
        /// <returns>Список всіх країн.</returns>
        public List<Country> GetAllCountries()
        {
            var countries = new List<Country>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT CountryID, Name, Area, Population, GovernmentForm, CapitalCityName, ContinentID FROM Countries ORDER BY Name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countries.Add(new Country
                            {
                                CountryID = Convert.ToInt64(reader["CountryID"]),
                                Name = reader["Name"].ToString(),
                                Area = reader["Area"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["Area"]),
                                Population = reader["Population"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["Population"]),
                                GovernmentForm = reader["GovernmentForm"].ToString(),
                                CapitalCityName = reader["CapitalCityName"].ToString(),
                                ContinentID = Convert.ToInt64(reader["ContinentID"])
                            });
                        }
                    }
                }
            }
            return countries;
        }

        /// <summary>
        /// Отримує країни за ID материка.
        /// </summary>
        /// <param name="continentId">ID материка.</param>
        /// <returns>Список країн, що належать вказаному материку.</returns>
        public List<Country> GetCountriesByContinentId(long continentId)
        {
            var countries = new List<Country>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT CountryID, Name, Area, Population, GovernmentForm, CapitalCityName, ContinentID FROM Countries WHERE ContinentID = @ContinentID ORDER BY Name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContinentID", continentId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countries.Add(new Country
                            {
                                CountryID = Convert.ToInt64(reader["CountryID"]),
                                Name = reader["Name"].ToString(),
                                Area = reader["Area"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["Area"]),
                                Population = reader["Population"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["Population"]),
                                GovernmentForm = reader["GovernmentForm"].ToString(),
                                CapitalCityName = reader["CapitalCityName"].ToString(),
                                ContinentID = Convert.ToInt64(reader["ContinentID"])
                            });
                        }
                    }
                }
            }
            return countries;
        }


        /// <summary>
        /// Додає нову країну до бази даних.
        /// </summary>
        /// <param name="country">Об'єкт країни для додавання.</param>
        /// <returns>True, якщо додавання успішне, інакше false.</returns>
        public bool AddCountry(Country country)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Countries (Name, Area, Population, GovernmentForm, CapitalCityName, ContinentID) VALUES (@Name, @Area, @Population, @GovernmentForm, @CapitalCityName, @ContinentID);";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", country.Name);
                        command.Parameters.AddWithValue("@Area", country.Area.HasValue ? (object)country.Area.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@Population", country.Population.HasValue ? (object)country.Population.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@GovernmentForm", string.IsNullOrEmpty(country.GovernmentForm) ? DBNull.Value : (object)country.GovernmentForm);
                        command.Parameters.AddWithValue("@CapitalCityName", string.IsNullOrEmpty(country.CapitalCityName) ? DBNull.Value : (object)country.CapitalCityName);
                        command.Parameters.AddWithValue("@ContinentID", country.ContinentID);
                        command.ExecuteNonQuery();
                        country.CountryID = connection.LastInsertRowId;
                        return true;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    MessageBox.Show($"Помилка: Країна з назвою '{country.Name}' вже існує.", "Помилка додавання", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.Contains("FOREIGN KEY constraint failed"))
                {
                    MessageBox.Show($"Помилка: Вказаний материк (ID: {country.ContinentID}) не існує.", "Помилка зовнішнього ключа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Помилка SQLite: {ex.Message}", "Помилка бази даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загальна помилка: {ex.Message}", "Помилка додавання", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Оновлює існуючу країну в базі даних.
        /// </summary>
        /// <param name="country">Об'єкт країни з оновленими даними.</param>
        /// <returns>True, якщо оновлення успішне, інакше false.</returns>
        public bool UpdateCountry(Country country)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"UPDATE Countries SET 
                                        Name = @Name, 
                                        Area = @Area, 
                                        Population = @Population, 
                                        GovernmentForm = @GovernmentForm, 
                                        CapitalCityName = @CapitalCityName, 
                                        ContinentID = @ContinentID 
                                     WHERE CountryID = @CountryID;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", country.Name);
                        command.Parameters.AddWithValue("@Area", country.Area.HasValue ? (object)country.Area.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@Population", country.Population.HasValue ? (object)country.Population.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@GovernmentForm", string.IsNullOrEmpty(country.GovernmentForm) ? DBNull.Value : (object)country.GovernmentForm);
                        command.Parameters.AddWithValue("@CapitalCityName", string.IsNullOrEmpty(country.CapitalCityName) ? DBNull.Value : (object)country.CapitalCityName);
                        command.Parameters.AddWithValue("@ContinentID", country.ContinentID);
                        command.Parameters.AddWithValue("@CountryID", country.CountryID);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed"))
                {
                    MessageBox.Show($"Помилка: Країна з назвою '{country.Name}' вже існує.", "Помилка оновлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.Contains("FOREIGN KEY constraint failed"))
                {
                    MessageBox.Show($"Помилка: Вказаний материк (ID: {country.ContinentID}) не існує.", "Помилка зовнішнього ключа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Помилка SQLite: {ex.Message}", "Помилка бази даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загальна помилка: {ex.Message}", "Помилка оновлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Видаляє країну з бази даних за її ID.
        /// </summary>
        /// <param name="countryId">ID країни для видалення.</param>
        /// <returns>True, якщо видалення успішне, інакше false.</returns>
        public bool DeleteCountry(long countryId)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    // Перевірка наявності пов'язаних регіонів
                    string checkRegionsQuery = "SELECT COUNT(*) FROM Regions WHERE CountryID = @CountryID;";
                    using (var checkCmd = new SQLiteCommand(checkRegionsQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@CountryID", countryId);
                        if ((long)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Неможливо видалити країну, оскільки вона містить регіони. Спочатку видаліть або перепризначте регіони.", "Помилка видалення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    // Перевірка наявності пов'язаних міст
                    string checkCitiesQuery = "SELECT COUNT(*) FROM Cities WHERE CountryID = @CountryID;";
                    using (var checkCmd = new SQLiteCommand(checkCitiesQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@CountryID", countryId);
                        if ((long)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Неможливо видалити країну, оскільки вона містить міста. Спочатку видаліть або перепризначте міста.", "Помилка видалення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    string query = "DELETE FROM Countries WHERE CountryID = @CountryID;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryID", countryId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка видалення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Region Operations

        /// <summary>
        /// Отримує всі регіони з бази даних.
        /// </summary>
        /// <returns>Список всіх регіонів.</returns>
        public List<Region> GetAllRegions()
        {
            var regions = new List<Region>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT RegionID, Name, Type, Population, CapitalCityName, CountryID FROM Regions ORDER BY Name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            regions.Add(new Region
                            {
                                RegionID = Convert.ToInt64(reader["RegionID"]),
                                Name = reader["Name"].ToString(),
                                Type = reader["Type"].ToString(),
                                Population = reader["Population"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["Population"]),
                                CapitalCityName = reader["CapitalCityName"].ToString(),
                                CountryID = Convert.ToInt64(reader["CountryID"])
                            });
                        }
                    }
                }
            }
            return regions;
        }

        /// <summary>
        /// Отримує регіони за ID країни.
        /// </summary>
        /// <param name="countryId">ID країни.</param>
        /// <returns>Список регіонів, що належать вказаній країні.</returns>
        public List<Region> GetRegionsByCountryId(long countryId)
        {
            var regions = new List<Region>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT RegionID, Name, Type, Population, CapitalCityName, CountryID FROM Regions WHERE CountryID = @CountryID ORDER BY Name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryID", countryId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            regions.Add(new Region
                            {
                                RegionID = Convert.ToInt64(reader["RegionID"]),
                                Name = reader["Name"].ToString(),
                                Type = reader["Type"].ToString(),
                                Population = reader["Population"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["Population"]),
                                CapitalCityName = reader["CapitalCityName"].ToString(),
                                CountryID = Convert.ToInt64(reader["CountryID"])
                            });
                        }
                    }
                }
            }
            return regions;
        }

        /// <summary>
        /// Додає новий регіон до бази даних.
        /// </summary>
        /// <param name="region">Об'єкт регіону для додавання.</param>
        /// <returns>True, якщо додавання успішне, інакше false.</returns>
        public bool AddRegion(Region region)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Regions (Name, Type, Population, CapitalCityName, CountryID) VALUES (@Name, @Type, @Population, @CapitalCityName, @CountryID);";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", region.Name);
                        command.Parameters.AddWithValue("@Type", string.IsNullOrEmpty(region.Type) ? DBNull.Value : (object)region.Type);
                        command.Parameters.AddWithValue("@Population", region.Population.HasValue ? (object)region.Population.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@CapitalCityName", string.IsNullOrEmpty(region.CapitalCityName) ? DBNull.Value : (object)region.CapitalCityName);
                        command.Parameters.AddWithValue("@CountryID", region.CountryID);
                        command.ExecuteNonQuery();
                        region.RegionID = connection.LastInsertRowId;
                        return true;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // В SQLite немає окремого коду помилки для унікальності на рівні регіону, якщо вона не задана явно
                // Потрібно перевіряти унікальність програмно або додавати UNIQUE constraint на (Name, CountryID)
                if (ex.Message.Contains("FOREIGN KEY constraint failed"))
                {
                    MessageBox.Show($"Помилка: Вказана країна (ID: {region.CountryID}) не існує.", "Помилка зовнішнього ключа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Помилка SQLite: {ex.Message}", "Помилка бази даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загальна помилка: {ex.Message}", "Помилка додавання", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Оновлює існуючий регіон у базі даних.
        /// </summary>
        /// <param name="region">Об'єкт регіону з оновленими даними.</param>
        /// <returns>True, якщо оновлення успішне, інакше false.</returns>
        public bool UpdateRegion(Region region)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"UPDATE Regions SET 
                                        Name = @Name, 
                                        Type = @Type, 
                                        Population = @Population, 
                                        CapitalCityName = @CapitalCityName, 
                                        CountryID = @CountryID 
                                     WHERE RegionID = @RegionID;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", region.Name);
                        command.Parameters.AddWithValue("@Type", string.IsNullOrEmpty(region.Type) ? DBNull.Value : (object)region.Type);
                        command.Parameters.AddWithValue("@Population", region.Population.HasValue ? (object)region.Population.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@CapitalCityName", string.IsNullOrEmpty(region.CapitalCityName) ? DBNull.Value : (object)region.CapitalCityName);
                        command.Parameters.AddWithValue("@CountryID", region.CountryID);
                        command.Parameters.AddWithValue("@RegionID", region.RegionID);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("FOREIGN KEY constraint failed"))
                {
                    MessageBox.Show($"Помилка: Вказана країна (ID: {region.CountryID}) не існує.", "Помилка зовнішнього ключа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Помилка SQLite: {ex.Message}", "Помилка бази даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загальна помилка: {ex.Message}", "Помилка оновлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Видаляє регіон з бази даних за його ID.
        /// </summary>
        /// <param name="regionId">ID регіону для видалення.</param>
        /// <returns>True, якщо видалення успішне, інакше false.</returns>
        public bool DeleteRegion(long regionId)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    // Перевірка наявності пов'язаних міст
                    string checkCitiesQuery = "SELECT COUNT(*) FROM Cities WHERE RegionID = @RegionID;";
                    using (var checkCmd = new SQLiteCommand(checkCitiesQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@RegionID", regionId);
                        if ((long)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Неможливо видалити регіон, оскільки він містить міста. Спочатку видаліть або перепризначте міста.", "Помилка видалення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    string query = "DELETE FROM Regions WHERE RegionID = @RegionID;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RegionID", regionId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка видалення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region City Operations

        /// <summary>
        /// Отримує всі міста з бази даних.
        /// </summary>
        /// <returns>Список всіх міст.</returns>
        public List<City> GetAllCities()
        {
            var cities = new List<City>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT CityID, Name, Latitude, Longitude, Population, RegionID, CountryID FROM Cities ORDER BY Name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new City
                            {
                                CityID = Convert.ToInt64(reader["CityID"]),
                                Name = reader["Name"].ToString(),
                                Latitude = Convert.ToDouble(reader["Latitude"]),
                                Longitude = Convert.ToDouble(reader["Longitude"]),
                                Population = reader["Population"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["Population"]),
                                RegionID = reader["RegionID"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["RegionID"]),
                                CountryID = Convert.ToInt64(reader["CountryID"])
                            });
                        }
                    }
                }
            }
            return cities;
        }

        /// <summary>
        /// Отримує міста за ID країни.
        /// </summary>
        /// <param name="countryId">ID країни.</param>
        /// <returns>Список міст, що належать вказаній країні.</returns>
        public List<City> GetCitiesByCountryId(long countryId)
        {
            var cities = new List<City>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT CityID, Name, Latitude, Longitude, Population, RegionID, CountryID FROM Cities WHERE CountryID = @CountryID ORDER BY Name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryID", countryId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new City
                            {
                                CityID = Convert.ToInt64(reader["CityID"]),
                                Name = reader["Name"].ToString(),
                                Latitude = Convert.ToDouble(reader["Latitude"]),
                                Longitude = Convert.ToDouble(reader["Longitude"]),
                                Population = reader["Population"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["Population"]),
                                RegionID = reader["RegionID"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["RegionID"]),
                                CountryID = Convert.ToInt64(reader["CountryID"])
                            });
                        }
                    }
                }
            }
            return cities;
        }

        /// <summary>
        /// Отримує міста за ID регіону.
        /// </summary>
        /// <param name="regionId">ID регіону.</param>
        /// <returns>Список міст, що належать вказаному регіону.</returns>
        public List<City> GetCitiesByRegionId(long regionId)
        {
            var cities = new List<City>();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT CityID, Name, Latitude, Longitude, Population, RegionID, CountryID FROM Cities WHERE RegionID = @RegionID ORDER BY Name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RegionID", regionId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new City
                            {
                                CityID = Convert.ToInt64(reader["CityID"]),
                                Name = reader["Name"].ToString(),
                                Latitude = Convert.ToDouble(reader["Latitude"]),
                                Longitude = Convert.ToDouble(reader["Longitude"]),
                                Population = reader["Population"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["Population"]),
                                RegionID = reader["RegionID"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["RegionID"]),
                                CountryID = Convert.ToInt64(reader["CountryID"])
                            });
                        }
                    }
                }
            }
            return cities;
        }


        /// <summary>
        /// Додає нове місто до бази даних.
        /// </summary>
        /// <param name="city">Об'єкт міста для додавання.</param>
        /// <returns>True, якщо додавання успішне, інакше false.</returns>
        public bool AddCity(City city)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Cities (Name, Latitude, Longitude, Population, RegionID, CountryID) VALUES (@Name, @Latitude, @Longitude, @Population, @RegionID, @CountryID);";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", city.Name);
                        command.Parameters.AddWithValue("@Latitude", city.Latitude);
                        command.Parameters.AddWithValue("@Longitude", city.Longitude);
                        command.Parameters.AddWithValue("@Population", city.Population.HasValue ? (object)city.Population.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@RegionID", city.RegionID.HasValue ? (object)city.RegionID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@CountryID", city.CountryID);
                        command.ExecuteNonQuery();
                        city.CityID = connection.LastInsertRowId;
                        return true;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // В SQLite немає окремого коду помилки для унікальності на рівні міста, якщо вона не задана явно
                // Потрібно перевіряти унікальність програмно або додавати UNIQUE constraint на (Name, CountryID)
                if (ex.Message.Contains("FOREIGN KEY constraint failed"))
                {
                    MessageBox.Show($"Помилка: Вказана країна (ID: {city.CountryID}) або регіон (ID: {city.RegionID}) не існує.", "Помилка зовнішнього ключа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.Contains("NOT NULL constraint failed: Cities.Latitude") || ex.Message.Contains("NOT NULL constraint failed: Cities.Longitude"))
                {
                    MessageBox.Show($"Помилка: Координати (широта та довгота) є обов'язковими для міста.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Помилка SQLite: {ex.Message}", "Помилка бази даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загальна помилка: {ex.Message}", "Помилка додавання", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Оновлює існуюче місто в базі даних.
        /// </summary>
        /// <param name="city">Об'єкт міста з оновленими даними.</param>
        /// <returns>True, якщо оновлення успішне, інакше false.</returns>
        public bool UpdateCity(City city)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = @"UPDATE Cities SET 
                                        Name = @Name, 
                                        Latitude = @Latitude, 
                                        Longitude = @Longitude, 
                                        Population = @Population, 
                                        RegionID = @RegionID, 
                                        CountryID = @CountryID 
                                     WHERE CityID = @CityID;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", city.Name);
                        command.Parameters.AddWithValue("@Latitude", city.Latitude);
                        command.Parameters.AddWithValue("@Longitude", city.Longitude);
                        command.Parameters.AddWithValue("@Population", city.Population.HasValue ? (object)city.Population.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@RegionID", city.RegionID.HasValue ? (object)city.RegionID.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@CountryID", city.CountryID);
                        command.Parameters.AddWithValue("@CityID", city.CityID);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                if (ex.Message.Contains("FOREIGN KEY constraint failed"))
                {
                    MessageBox.Show($"Помилка: Вказана країна (ID: {city.CountryID}) або регіон (ID: {city.RegionID}) не існує.", "Помилка зовнішнього ключа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.Contains("NOT NULL constraint failed: Cities.Latitude") || ex.Message.Contains("NOT NULL constraint failed: Cities.Longitude"))
                {
                    MessageBox.Show($"Помилка: Координати (широта та довгота) є обов'язковими для міста.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Помилка SQLite: {ex.Message}", "Помилка бази даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Загальна помилка: {ex.Message}", "Помилка оновлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Видаляє місто з бази даних за його ID.
        /// </summary>
        /// <param name="cityId">ID міста для видалення.</param>
        /// <returns>True, якщо видалення успішне, інакше false.</returns>
        public bool DeleteCity(long cityId)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Cities WHERE CityID = @CityID;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CityID", cityId);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка видалення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Search and Reports

        /// <summary>
        /// Шукає міста за заданими критеріями.
        /// </summary>
        /// <param name="searchTerm">Термін для пошуку.</param>
        /// <param name="searchCriteria">Критерій пошуку ("Назва міста", "Назва країни", "Назва регіону").</param>
        /// <returns>Список знайдених міст.</returns>
        public List<City> SearchCities(string searchTerm, string searchCriteria)
        {
            var cities = new List<City>();
            string query = @"SELECT DISTINCT ci.CityID, ci.Name, ci.Latitude, ci.Longitude, ci.Population, ci.RegionID, ci.CountryID 
                             FROM Cities ci ";

            if (searchCriteria == "Назва країни")
            {
                query += " JOIN Countries co ON ci.CountryID = co.CountryID WHERE co.Name LIKE @SearchTerm ";
            }
            else if (searchCriteria == "Назва регіону")
            {
                query += " JOIN Regions r ON ci.RegionID = r.RegionID WHERE r.Name LIKE @SearchTerm ";
            }
            else // "Назва міста" або за замовчуванням
            {
                query += " WHERE ci.Name LIKE @SearchTerm ";
            }
            query += " ORDER BY ci.Name;";


            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new City
                            {
                                CityID = Convert.ToInt64(reader["CityID"]),
                                Name = reader["Name"].ToString(),
                                Latitude = Convert.ToDouble(reader["Latitude"]),
                                Longitude = Convert.ToDouble(reader["Longitude"]),
                                Population = reader["Population"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["Population"]),
                                RegionID = reader["RegionID"] == DBNull.Value ? (long?)null : Convert.ToInt64(reader["RegionID"]),
                                CountryID = Convert.ToInt64(reader["CountryID"])
                            });
                        }
                    }
                }
            }
            return cities;
        }

        /// <summary>
        /// Отримує дані для звіту про населеність материків.
        /// </summary>
        /// <returns>Список об'єктів з даними для звіту.</returns>
        public List<dynamic> GetContinentPopulationDensityReport()
        {
            var reportData = new List<dynamic>();
            string query = @"
                SELECT 
                    con.Name AS ContinentName,
                    SUM(cou.Area) AS TotalArea,
                    SUM(cou.Population) AS TotalPopulation
                FROM Continents con
                JOIN Countries cou ON con.ContinentID = cou.ContinentID
                WHERE cou.Area IS NOT NULL AND cou.Area > 0 AND cou.Population IS NOT NULL 
                GROUP BY con.ContinentID, con.Name
                ORDER BY con.Name;";

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string continentName = reader["ContinentName"].ToString();
                            double totalArea = Convert.ToDouble(reader["TotalArea"]);
                            long totalPopulation = Convert.ToInt64(reader["TotalPopulation"]);
                            double density = (totalArea > 0) ? (totalPopulation / totalArea) : 0;

                            reportData.Add(new
                            {
                                ContinentName = continentName,
                                TotalArea = totalArea,
                                TotalPopulation = totalPopulation,
                                Density = density
                            });
                        }
                    }
                }
            }
            return reportData;
        }

        #endregion
    }
}
