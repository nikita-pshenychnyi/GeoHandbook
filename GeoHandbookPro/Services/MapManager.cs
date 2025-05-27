using System;
using System.Collections.Generic;
using System.Drawing; // Для Brushes, Pens
using System.IO; // Для Path
using System.Linq; // Для Any()
using System.Windows.Forms; // Для Application, MessageBox
using GeoHandbookPro.Models;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace GeoHandbookPro.Services
{
    /// <summary>
    /// Керує операціями, пов'язаними з картою GMap.NET.
    /// </summary>
    public class MapManager
    {
        private readonly GMapControl _gmapControl;
        private GMapOverlay _markersOverlay; // Шар для маркерів міст
        private GMapOverlay _polygonsOverlay; // Шар для полігонів (наприклад, країн, якщо буде реалізовано)

        private const string MarkersOverlayId = "markers";
        private const string PolygonsOverlayId = "polygons";

        /// <summary>
        /// Ініціалізує новий екземпляр класу MapManager.
        /// </summary>
        /// <param name="gmapControl">Елемент керування GMapControl, яким буде керувати цей менеджер.</param>
        public MapManager(GMapControl gmapControl)
        {
            _gmapControl = gmapControl ?? throw new ArgumentNullException(nameof(gmapControl));
            InitializeMap();
        }

        /// <summary>
        /// Ініціалізує основні параметри карти.
        /// </summary>
        private void InitializeMap()
        {
            // Налаштування провайдера карти
            _gmapControl.MapProvider = GMapProviders.OpenStreetMap; // Або GoogleMap, BingMap тощо.

            // Початкова позиція та масштабування
            _gmapControl.Position = new PointLatLng(48.3794, 31.1656); // Приблизний центр України
            _gmapControl.MinZoom = 2;
            _gmapControl.MaxZoom = 18;
            _gmapControl.Zoom = 6;

            // Інтерактивність
            _gmapControl.DragButton = MouseButtons.Left; // Перетягування картою лівою кнопкою миші
            _gmapControl.ShowCenter = false; // Не показувати червоний хрестик у центрі

            // Налаштування кешування
            ConfigureCache();

            // Створення та додавання шарів для маркерів та полігонів
            _markersOverlay = new GMapOverlay(MarkersOverlayId);
            _polygonsOverlay = new GMapOverlay(PolygonsOverlayId);

            _gmapControl.Overlays.Add(_markersOverlay);
            _gmapControl.Overlays.Add(_polygonsOverlay);

            // Додавання обробника події для отримання координат при кліку (опціонально)
            // _gmapControl.MouseClick += GmapControl_MouseClick;
        }

        /// <summary>
        /// Налаштовує кешування тайлів карти.
        /// </summary>
        private void ConfigureCache()
        {
            try
            {
                // Спроба перевірити доступ до Інтернету
                System.Net.Dns.GetHostEntry("www.openstreetmap.org"); // Або інший надійний хост
                _gmapControl.Manager.Mode = AccessMode.ServerAndCache; // Завантажувати з сервера та кешувати
            }
            catch
            {
                _gmapControl.Manager.Mode = AccessMode.CacheOnly; // Працювати тільки з кешем
                MessageBox.Show("Немає підключення до Інтернету. Карта працюватиме в офлайн-режимі з кешу, якщо він доступний.",
                                "Режим роботи карти", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Вказуємо місце для збереження кешу карти
            string cachePath = Path.Combine(Application.StartupPath, "GMapCache");
            if (!Directory.Exists(cachePath))
            {
                try
                {
                    Directory.CreateDirectory(cachePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося створити папку для кешу карти: {ex.Message}", "Помилка кешування", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Можна встановити режим без кешування або інший шлях
                    _gmapControl.Manager.Mode = AccessMode.ServerOnly; // Або вимкнути кеш
                    return;
                }
            }
            _gmapControl.CacheLocation = cachePath;
        }

        /// <summary>
        /// Відображає список міст на карті у вигляді маркерів.
        /// </summary>
        /// <param name="cities">Список об'єктів City для відображення.</param>
        /// <param name="clearPreviousMarkers">Прапорець, що вказує, чи потрібно очищати попередні маркери.</param>
        public void PlotCities(IEnumerable<City> cities, bool clearPreviousMarkers = true)
        {
            if (clearPreviousMarkers)
            {
                ClearMarkers();
            }

            if (cities == null || !cities.Any())
            {
                _gmapControl.Refresh(); // Оновити карту, навіть якщо немає міст для відображення (для очищення)
                return;
            }

            foreach (var city in cities)
            {
                PointLatLng point = new PointLatLng(city.Latitude, city.Longitude);
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_small)
                {
                    ToolTipText = $"Місто: {city.Name}\nНаселення: {city.Population?.ToString("N0") ?? "N/A"}\nКоординати: {city.Latitude:F4}, {city.Longitude:F4}",
                    Tag = city.CityID // Зберігаємо ID міста в тезі маркера для подальшої ідентифікації
                };
                _markersOverlay.Markers.Add(marker);
            }

            // Опціонально: центрувати карту на першому місті або на області, що охоплює всі міста
            if (cities.Any())
            {
                // CenterMapOnPoint(cities.First().Latitude, cities.First().Longitude, 10);
                ZoomToFitMarkers();
            }
            _gmapControl.Refresh(); // Оновити карту для відображення змін
        }

        /// <summary>
        /// Очищає всі маркери з шару маркерів.
        /// </summary>
        public void ClearMarkers()
        {
            if (_markersOverlay != null)
            {
                _markersOverlay.Markers.Clear();
                _gmapControl.Refresh();
            }
        }

        /// <summary>
        /// Очищає всі полігони з шару полігонів.
        /// </summary>
        public void ClearPolygons()
        {
            if (_polygonsOverlay != null)
            {
                _polygonsOverlay.Polygons.Clear();
                _gmapControl.Refresh();
            }
        }

        /// <summary>
        /// Очищає всі об'єкти (маркери, полігони) з карти.
        /// </summary>
        public void ClearAllOverlays()
        {
            ClearMarkers();
            ClearPolygons();
            // Можна додати очищення інших шарів, якщо вони будуть
        }

        /// <summary>
        /// Центрує карту на заданій точці з певним рівнем масштабування.
        /// </summary>
        /// <param name="latitude">Широта центральної точки.</param>
        /// <param name="longitude">Довгота центральної точки.</param>
        /// <param name="zoomLevel">Рівень масштабування (від MinZoom до MaxZoom).</param>
        public void CenterMapOnPoint(double latitude, double longitude, int zoomLevel)
        {
            _gmapControl.Position = new PointLatLng(latitude, longitude);
            _gmapControl.Zoom = zoomLevel;
        }

        /// <summary>
        /// Масштабує карту так, щоб вмістити всі поточні маркери на шарі _markersOverlay.
        /// </summary>
        public void ZoomToFitMarkers()
        {
            if (_markersOverlay.Markers.Any())
            {
                _gmapControl.ZoomAndCenterMarkers(_markersOverlay.Id);
            }
            else if (_gmapControl.Overlays.Count > 0 && _gmapControl.Overlays[0].Markers.Any()) // Спроба для першого шару, якщо _markersOverlay порожній
            {
                _gmapControl.ZoomAndCenterMarkers(_gmapControl.Overlays[0].Id);
            }
        }


        // Приклад методу для відображення полігону (наприклад, для країни)
        // Потребує списку точок PointLatLng, що утворюють контур полігону.
        // Дані про контури країн потрібно отримувати з зовнішніх джерел (GeoJSON, KML, Shapefile).
        /*
        public void PlotPolygon(List<PointLatLng> points, string name, Color fillColor, Color strokeColor)
        {
            if (points == null || points.Count < 3) return;

            GMapPolygon polygon = new GMapPolygon(points, name)
            {
                Fill = new SolidBrush(Color.FromArgb(50, fillColor)), // Напівпрозоре заповнення
                Stroke = new Pen(strokeColor, 1)
            };
            _polygonsOverlay.Polygons.Add(polygon);
            _gmapControl.Refresh();
        }
        */

        // Обробник кліку на карті для отримання координат (якщо потрібно)
        /*
        private void GmapControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Наприклад, правий клік
            {
                PointLatLng point = _gmapControl.FromLocalToLatLng(e.X, e.Y);
                MessageBox.Show($"Координати: {point.Lat:F6}, {point.Lng:F6}", "Клік на карті");
            }
        }
        */
    }
}
