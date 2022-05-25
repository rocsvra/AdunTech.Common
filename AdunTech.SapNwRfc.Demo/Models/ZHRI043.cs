namespace AdunTech.SapNwRfc.Demo.Models
{
    public class ZHRI043A
    {
        public string PERNR { get; set; }
        public string MONTH { get; set; }
        public decimal STDAZ { get; set; }
        public decimal STDAZ_A { get; set; }
        public decimal ABZHL { get; set; }
        public string ZBNUM { get; set; }
        public string ZFLAG { get; set; }
    }

    public class ZHRI043B
    {
        public string PERNR { get; set; }
        public string DATE { get; set; }
        public string WOTNR { get; set; }
        public string ETEXT { get; set; }
        public string BEGTM { get; set; }
        public string ENDTM { get; set; }
    }

    public class ZHRI043E
    {
        public string PERNR { get; set; }
        public string LDATE { get; set; }
        public string LTIME { get; set; }
    }

    public class ZHRI043_Input
    {
        public string IM_PERNR { get; set; }
        public string IM_MONTH { get; set; }
    }

    public class ZHRI043_Output : SapOutputBase
    {
        public ZHRI043A[] ZHRI043A { get; set; }
        public ZHRI043B[] ZHRI043B { get; set; }
        public ZHRI043E[] ZHRI043E { get; set; }
    }
}
