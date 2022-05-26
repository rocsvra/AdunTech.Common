using NPoco;

namespace AdunTech.NpocoDemo.Models
{
    [TableName("TestA")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class TestA
    {
        [Column]
        public string Id { get; set; }
        [Column]
        public string A { get; set; }
    }
}
