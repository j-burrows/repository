using System;
namespace Repository.Helpers{
    public class Time{
        public int Hour { get; set; }
        public int Millisecond { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public long Ticks { get; set; }
        public TimeSpan TimeOfDay { get; set; }
        public static DateTime UtcNow { get; set; }
    }
}
