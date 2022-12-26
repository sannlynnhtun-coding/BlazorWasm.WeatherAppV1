using BlazorWASM.WeatherApp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http.Json;
using System.Reflection;

namespace BlazorWASM.WeatherApp.Pages
{
    public partial class Index
    {
        [Inject] HttpClient _httpClient { get; set; }

        private OpenMeteoWeatherResponseModel model { get; set; }

        private CustomCurrentWeatherModel current { get; set; } = new CustomCurrentWeatherModel();

        private List<CustomDailyWeatherModel> daily { get; set; } = new List<CustomDailyWeatherModel>();

        private List<CustomHourlyWeatherModel> hourly { get; set; } = new List<CustomHourlyWeatherModel>();

        protected override async Task OnInitializedAsync()
        {
            await GetWeather(16.8496, 96.1292, "Asia/Rangoon");
        }

        public async Task GetWeather(double lat, double lon, string timezone)
        {
            string endpoints = $"https://api.open-meteo.com/v1/forecast?hourly=temperature_2m,apparent_temperature,precipitation,weathercode,windspeed_10m&daily=weathercode,temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,precipitation_sum&current_weather=true&temperature_unit=fahrenheit&windspeed_unit=mph&precipitation_unit=inch&timeformat=unixtime&latitude={lat}&longitude={lon}&timezone={timezone}";
            model = await _httpClient.GetFromJsonAsync<OpenMeteoWeatherResponseModel>(endpoints);

            var cw = model.current_weather;
            var d = model.daily;
            var h = model.hourly;
            current = new CustomCurrentWeatherModel
            {
                currentTemp = Math.Round(cw.temperature),
                highTemp = Math.Round(d.temperature_2m_max.Max()),
                lowTemp = Math.Round(d.temperature_2m_min.Max()),
                highFeelsLike = Math.Round(d.apparent_temperature_max.Max()),
                lowFeelsLike = Math.Round(d.apparent_temperature_min.Min()),
                windSpeed = Math.Round(cw.windspeed),
                precip = Math.Round(d.precipitation_sum.Max() * 100) / 100,
                iconCode = cw.weathercode,
            };

            for (int i = 0; i < d.time.Length; i++)
            {
                daily.Add(new CustomDailyWeatherModel
                {
                    timestamp = d.time[i] * 1000,
                    iconCode = d.weathercode[i],
                    maxTemp = Math.Round(d.temperature_2m_max[i])
                });
            }

            for (int i = 0; i < h.time.Length; i++)
            {
                hourly.Add(new CustomHourlyWeatherModel
                {
                    timestamp = h.time[i] * 1000,
                    iconCode = h.weathercode[i],
                    temp = Math.Round(h.temperature_2m[i]),
                    feelsLike = Math.Round(h.apparent_temperature[i]),
                    windSpeed = Math.Round(h.windspeed_10m[i]),
                    precip = Math.Round(h.precipitation[i] * 100) / 100,
                });
            }
        }
    }
}
