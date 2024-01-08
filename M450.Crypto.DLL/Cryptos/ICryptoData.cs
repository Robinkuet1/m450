namespace M450.Crypto.DLL.Cryptos;

public interface ICryptoData
{
    public string Currency { get; }
    public decimal GetCurrentPrice();
    public decimal GetTransactionVolume(TimeSpan s);
    public decimal GetTransactionFees(TimeSpan s);
}