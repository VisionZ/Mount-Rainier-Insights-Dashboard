namespace MountRainerInsights
{

    public interface CsvSerializable
    {
        bool Import(string[] exportedData);
    }

    public class ClimbInfo : CsvSerializable
    {
        public ClimbInfo()
        {

        }

        public string Date
        {
            get; set;
        }

        public string Route
        {
            get; set;
        }

        public int Attempted
        {
            get; set;
        }

        public int Succeeded
        {
            get; set;
        }

        public double SuccessPercentage
        {
            get
            {
                if (Attempted == 0)
                {
                    return 0;
                }
                return ((double)Succeeded) / ((double)Attempted) * 100.0;
            }
        }

        public double FailedPercentage
        {
            get
            {
                if (Attempted == 0)
                {
                    return 0;
                }
                return (1.0 - ((double)Succeeded) / ((double)Attempted)) * 100.0;
            }
        }

        public bool Import(string[] data)
        {
            if (data.Length != 4)
            {
                return false;
            }

            string parsedDate = Util.ParseDate(Util.DEFAULT_DATE_FORMATS, Util.DEFAULT_DATE_DELIMITERS, data[0]);
            if (parsedDate == null)
            {
                return false;
            }

            int Attempted;
            if (!int.TryParse(data[2], out Attempted))
            {
                return false;
            }

            int Succeeded;
            if (!int.TryParse(data[3], out Succeeded))
            {
                return false;
            }

            this.Date = parsedDate;
            this.Route = data[1];
            this.Attempted = Attempted;
            this.Succeeded = Succeeded;

            return true;
        }
    }

    public class WeatherInfo : CsvSerializable
    {

        public WeatherInfo()
        {

        }

        public string Date
        {
            get; set;
        }

        public double AverageBatteryVoltage
        {
            get; set;
        }

        public double AverageTemperature
        {
            get; set;
        }

        public double AverageRelativeHumidity
        {
            get; set;
        }

        public double AverageDailyWindSpeed
        {
            get; set;
        }

        public double AverageWindDirection
        {
            get; set;
        }

        public double AverageSolarRadiation
        {
            get; set;
        }

        public bool Import(string[] data) {
            if (data.Length != 7) {
                return false;
            }

            string parsedDate = Util.ParseDate(Util.DEFAULT_DATE_FORMATS, Util.DEFAULT_DATE_DELIMITERS, data[0]);
            if (parsedDate == null) {
                return false;
            }

            double AverageBatteryVoltage;
            if (!double.TryParse(data[1], out AverageBatteryVoltage)) {
                return false;
            }

            double AverageTemperature;
            if (!double.TryParse(data[2], out AverageTemperature)) {
                return false;
            }

            double AverageRelativeHumidity;
            if (!double.TryParse(data[3], out AverageRelativeHumidity)) {
                return false;
            }

            double AverageDailyWindSpeed;
            if (!double.TryParse(data[4], out AverageDailyWindSpeed)) {
                return false;
            }

            double AverageWindDirection;
            if (!double.TryParse(data[5], out AverageWindDirection)) {
                return false;
            }

            double AverageSolarRadiation;
            if (!double.TryParse(data[6], out AverageSolarRadiation)) {
                return false;
            }

            this.Date = parsedDate;
            this.AverageBatteryVoltage = AverageBatteryVoltage;
            this.AverageTemperature = AverageTemperature;
            this.AverageRelativeHumidity = AverageRelativeHumidity;
            this.AverageDailyWindSpeed = AverageDailyWindSpeed;
            this.AverageWindDirection = AverageWindDirection;
            this.AverageSolarRadiation = AverageSolarRadiation;

            return true;
        }
    }
}