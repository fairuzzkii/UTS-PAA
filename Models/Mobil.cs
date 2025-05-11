namespace API_Dealer_Mobil_Acc.Models
{
    public class Mobil
    {
        public int Id { get; set; }
        public string Merek { get; set; }
        public string Model { get; set; }
        public int Tahun { get; set; }
        public long Harga { get; set; }
        public string Warna { get; set; }
    }
}
