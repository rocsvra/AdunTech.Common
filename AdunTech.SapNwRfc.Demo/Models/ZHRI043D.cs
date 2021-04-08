namespace AdunTech.SapNwRfc.Demo.Models
{
    public class ZHRI043D
    {
        public string PERNR { get; set; }
        public string ANZHL1 { get; set; }
        public string ANZHL2 { get; set; }
        public string ANZHL3 { get; set; }
    }

    public class ZHRI043D_Input
    {
        public string IM_PERNR { get; set; }
    }

    public class ZHRI043D_Output : SapOutputBase
    {
        public ZHRI043D[] ZHRI043D { get; set; }
    }
}
