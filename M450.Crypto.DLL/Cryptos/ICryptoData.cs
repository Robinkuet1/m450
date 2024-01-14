namespace M450.Crypto.DLL.Cryptos;

public interface ICryptoData
{
    public CryptoCurrency Currency { get; }
    public decimal GetCurrentPrice();
    public decimal GetPricePerDate(DateOnly date);
    public decimal GetTransactionVolume();
    public decimal GetTransactionFees();
}