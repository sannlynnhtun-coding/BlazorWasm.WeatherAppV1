namespace BlazorWasm.WeatherAppV1.Models
{
    public class OpenMeteoWeatherResponseModel
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public float elevation { get; set; }
        public CurrentWeatherModel current_weather { get; set; }
        public HourlyUnitsModel hourly_units { get; set; }
        public HourlyModel hourly { get; set; }
        public DailyUnitsModel daily_units { get; set; }
        public DailyModel daily { get; set; }
    }

    public class CurrentWeatherModel
    {
        public float temperature { get; set; }
        public float windspeed { get; set; }
        public float winddirection { get; set; }
        public int weathercode { get; set; }
        public int time { get; set; }
    }

    public class HourlyUnitsModel
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string apparent_temperature { get; set; }
        public string precipitation { get; set; }
        public string weathercode { get; set; }
        public string windspeed_10m { get; set; }
    }

    public class HourlyModel
    {
        public int[] time { get; set; }
        public float[] temperature_2m { get; set; }
        public float[] apparent_temperature { get; set; }
        public float[] precipitation { get; set; }
        public int[] weathercode { get; set; }
        public float[] windspeed_10m { get; set; }
    }

    public class DailyUnitsModel
    {
        public string time { get; set; }
        public string weathercode { get; set; }
        public string temperature_2m_max { get; set; }
        public string temperature_2m_min { get; set; }
        public string apparent_temperature_max { get; set; }
        public string apparent_temperature_min { get; set; }
        public string precipitation_sum { get; set; }
    }

    public class DailyModel
    {
        public int[] time { get; set; }
        public int[] weathercode { get; set; }
        public float[] temperature_2m_max { get; set; }
        public float[] temperature_2m_min { get; set; }
        public float[] apparent_temperature_max { get; set; }
        public float[] apparent_temperature_min { get; set; }
        public float[] precipitation_sum { get; set; }
    }

    public class CustomCurrentWeatherModel
    {
        public double currentTemp { get; set; }
        public double highTemp { get; set; }
        public double lowTemp { get; set; }
        public double highFeelsLike { get; set; }
        public double lowFeelsLike { get; set; }
        public double windSpeed { get; set; }
        public double precip { get; set; }
        public int iconCode { get; set; }
        public string iconCodeStr { get { return iconCode.GetIconMap(); } }
    }

    public class CustomDailyWeatherModel
    {
        public double timestamp { get; set; }
        public int iconCode { get; set; }
        public string iconCodeStr { get { return iconCode.GetIconMap(); } }
        public double maxTemp { get; set; }
    }

    public class CustomHourlyWeatherModel
    {
        public double timestamp { get; set; }
        public int iconCode { get; set; }
        public string iconCodeStr { get { return iconCode.GetIconMap(); } }
        public double temp { get; set; }
        public double feelsLike { get; set; }
        public double windSpeed { get; set; }
        public double precip { get; set; }
    }
}

public static class Extensions
{
    // https://stackoverflow.com/questions/20147879/switch-case-can-i-use-a-range-instead-of-a-one-number
    public static string GetIconMap(this int code)
    {
        return code switch
        {
            <= 1 => "sun",
            2 => "cloud-sun",
            3 => "cloud",
            45 => "smog",
            48 => "smog",
            51 => "cloud-showers-heavy",
            53 => "cloud-showers-heavy",
            55 => "cloud-showers-heavy",
            56 => "cloud-showers-heavy",
            57 => "cloud-showers-heavy",
            61 => "cloud-showers-heavy",
            63 => "cloud-showers-heavy",
            65 => "cloud-showers-heavy",
            66 => "cloud-showers-heavy",
            67 => "cloud-showers-heavy",
            80 => "cloud-showers-heavy",
            81 => "cloud-showers-heavy",
            82 => "cloud-showers-heavy",
            71 => "snowflake",
            73 => "snowflake",
            75 => "snowflake",
            77 => "snowflake",
            85 => "snowflake",
            86 => "snowflake",
            95 => "cloud-bolt",
            96 => "cloud-bolt",
            99 => "cloud-bolt",
            _ => ""
        };
    }

    // https://stackoverflow.com/questions/4687357/how-to-convert-javascript-numeric-date-into-c-sharp-date-using-c-not-javascri
    public static string ToDateTime(this double unixTimeStamp, string format)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp / 1000).ToLocalTime();
        return dateTime.ToString(format);
    }
}
