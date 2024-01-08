namespace M450.Crypto.DLL.Cryptos;

public interface ICryptoData
{
    public string Currency { get; }
    public decimal GetCurrentPrice();
    public decimal GetTransactionVolume();
    public decimal GetTransactionFees(TimeSpan s);
}