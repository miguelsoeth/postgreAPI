namespace postgreAPI.Models
{
    public class queryHistory
    {
        public int id { get; set; }
        public string username { get; set; }
        public string queryDate { get; set; }
        public string type { get; set; }
        public string document { get; set; }
        public string referredDate { get; set; }
        public string interval { get; set; }
    }
}
