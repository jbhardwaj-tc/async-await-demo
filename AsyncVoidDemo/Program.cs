namespace AsyncVoidDemo;

class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            await ThrowExceptionWithAsyncTask();
            ThrowExceptionWithAsyncVoid();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.Read();
    }

    /// <summary>
    /// Exception thrown by this method will be caught
    /// </summary>
    static async Task ThrowExceptionWithAsyncTask()
    {
        throw new Exception("Testing exception with async Task method.");
    }

    /// <summary>
    /// Exception thrown by this method will not be caught
    /// </summary>
    /// <remarks>BAD IDEA</remarks>
    static async void ThrowExceptionWithAsyncVoid()
    {
        throw new Exception("Testing exception with async void method.");
    }
}