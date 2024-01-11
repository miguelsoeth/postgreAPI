namespace postgreAPI.Dtos
{
    public class queryHistoryDto
    {
        public string username { get; set; }
        public string type { get; set; }
        public string document { get; set; }
        public DateTime referredDate { get; set; }
        public string interval { get; set; }
    }
}
