﻿namespace postgreAPI.Models
{
    public class queryHistoryModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public DateTime querydate { get; set; }
        public string type { get; set; }
        public string document { get; set; }
        public DateTime referreddate { get; set; }
        public string interval { get; set; }
    }
}
