namespace Cake.Alive
{
    public class AliveSettings
    {
        //private static readonly TimeSpan defaultTimeout = TimeSpan.FromSeconds(100.0);
        //private static readonly TimeSpan maxTimeout = TimeSpan.FromMilliseconds((double)int.MaxValue);
        //private static readonly TimeSpan infiniteTimeout = TimeSpan.FromMilliseconds(-1.0);

        //    if (value != HttpClient.infiniteTimeout && (value <= TimeSpan.Zero || value > HttpClient.maxTimeout))
        //throw new ArgumentOutOfRangeException("value");


        /// <summary>
        /// Timeout in milliseconds
        /// </summary>
        public int Timeout { get; set; }
    }
}