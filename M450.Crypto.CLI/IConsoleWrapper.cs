namespace M450.Crypto.CLI;

public interface IConsoleWrapper
{
    void WriteLine(string message);
    void WriteError(string errorMessage);
}