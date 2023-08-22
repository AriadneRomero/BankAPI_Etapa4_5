using System.Text.Json.Serialization;

namespace TestBankAPI.DTOS;

public class BankTDtoIn
{
    public int AccountId { get; set; }
    public int TransactionType { get; set; }
    public decimal Amount { get; set; }
    public int? ExternalAccount { get; set; }

}