using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace CSharpCodeSnippets.ExceptionHandling
{
    public class Result<T>
    {

        private static readonly ILog _log = LogManager.GetLogger(typeof(Result<T>));

        private T _value;
        private string _message;
        private bool _valid;

        Result() { }


        public static Result<T> Ret(T v)
        {
            Result<T> box = new Result<T>
            {
                _value = v,
                _valid = true
            };
            return box;
        }

        public static Result<T> Fail(string message)
        {
            Result<T> box = new Result<T>
            {
                _message = message,
                _valid = false
            };
            return box;
        }

        public static implicit operator bool(Result<T> v)
        {
            return v._valid;
        }

        public T Value
        {
            get
            {
                if (!this)
                {
                    string message = "User Error! Cannot deconstruct a value from an error!!";
                    _log.Error(message);
                    throw new Exception(message);
                }

                return _value;
            }
        }

        public string Message
        {
            get { return _message; }
        }

    }
}
