namespace AdunTech.SapRfc.Test
{
    public class ZHRI043D
    {
        public string PERNR { get; set; }
        public decimal ANZHL1 { get; set; }
        public decimal ANZHL2 { get; set; }
        public decimal ANZHL3 { get; set; }
    }

    public class ZHRI043D_Input
    {
        public string IM_PERNR { get; set; }
    }

    public class ZHRI043D_Output
    {
        public string MSGTYPE { get; set; }
        public string MESSAGE { get; set; }
        public ZHRI043D[] ZHRI043D { get; set; }
    }
}
