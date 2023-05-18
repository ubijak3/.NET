using Azure.Core;

namespace cwiczenia7_ko_ubijak3.DTO_s
{
    public class ClientToTripRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string pesel { get; set; }
        public int tripId { get; set; }
        public string tripName { get; set; }
        public DateTime paymentDate { get; set; }
    }
}
