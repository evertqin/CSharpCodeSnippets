using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace CSharpCodeSnippets.ExceptionHandling
{
    public class AsyncInvoker
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof (AsyncInvoker));


        public static async Task<Result<T>> Invoke<T>(Func<Task<T>> action, string failMessage = "")
        {
            try
            {
                T result = await action();
                return result != null ? Result<T>.Ret(result) : Result<T>.Fail(failMessage);
            }
            catch (Exception ex)
            {
                _log.Error(failMessage + "\n\n" +ex.Message + "\n\n" + ex.StackTrace);
                return Result<T>.Fail(ex.Message);
            }
        }

        public static async Task<Result<T>> Invoke<T, E>(Func<Task<T>> action, string failMessage = "") where E: Exception
        {
            try
            {
                T result = await action();
                return result != null ? Result<T>.Ret(result) : Result<T>.Fail(failMessage);
            }
            catch (E spExp)
            {
                return
                    Result<T>.Fail(failMessage);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message + "\n\n" + ex.StackTrace);
                return Result<T>.Fail(ex.Message);
            }
        }

        // how to use
        // lets first try as sample 
        public async Task<string> TestAsyncService()
        {
            return await Task.Run(() => "Sample");
        }
        
        // This is how to call it
        public async Task<Result<string>> TestInvoke()
        {
            return await Invoke<string, Exception>(async () => await TestAsyncService(), "Sample Error");
        }
    }
}
