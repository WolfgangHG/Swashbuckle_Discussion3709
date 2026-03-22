using System;
using System.ComponentModel;

namespace WebApiSwaggerVersion
{
    public class WeatherForecast
    {
        [DefaultValue(typeof(DateTime), "1900-12-31 20:00:00")]
        public DateTime Date { get; set; }

        [DefaultValue(22)]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [DefaultValue("Test")]
        public string Summary { get; set; }

        /// <summary>
        /// A sample enum value
        /// </summary>
        [DefaultValue(DayOfWeek.Wednesday)]
        public System.DayOfWeek EnumValue { get; set; } = DayOfWeek.Wednesday;
        /// <summary>
        /// A sample enum value
        /// </summary>
        [DefaultValue(nameof(DayOfWeek.Wednesday))]
        public System.DayOfWeek EnumValueWithStringDefault { get; set; } = DayOfWeek.Wednesday;
      }
}
