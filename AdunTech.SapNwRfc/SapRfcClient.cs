using SapNwRfc;
using SapNwRfc.Pooling;

namespace AdunTech.SapNwRfc
{
    public class SapRfcClient : ISapRfcClient
    {
        private readonly ISapPooledConnection _connection;

        public SapRfcClient(ISapPooledConnection connection)
        {
            //确认SAP RFC SDK 是否存在,不存在会抛出SapLibraryNotFoundException 
            SapLibrary.EnsureLibraryPresent(); 
            _connection = connection;
        }

        public TOutput Execute<TOutput, TInput>(TInput input, string rfcFunctionName)
        {
            return _connection.InvokeFunction<TOutput>(rfcFunctionName, input);
        }

        public void Execute<TInput>(TInput input, string rfcFunctionName)
        {
            _connection.InvokeFunction(rfcFunctionName, input);
        }

        public void Execute(string rfcFunctionName)
        {
            _connection.InvokeFunction(rfcFunctionName);
        }
    }
}
