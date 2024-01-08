namespace M450.Crypto.DLL.Cryptos;

public interface ICryptoData
{
    public decimal GetPrice();
    public decimal GetTransactionVolume(TimeSpan s);
    public decimal GetTransactionFees(TimeSpan s);
}