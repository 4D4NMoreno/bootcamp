using Core.Constants;

namespace Core.Request;

    public class BankProductRequest
    {
        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public CreateCreditProduct CreditProduct { get; set;}


}

