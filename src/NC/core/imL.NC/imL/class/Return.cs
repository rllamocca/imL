using System;

namespace imL.NC
{
    public class Return
    {
        readonly bool? _SUCCESS;
        readonly object? _RESULT;
        readonly string? _MESSAGE;
        readonly Exception? _EXCEPTION;

        public bool? Success { get { return _SUCCESS; } }
        public object? Result { get { return _RESULT; } }
        public string? Message { get { return _MESSAGE; } }
        public Exception? Exception { get { return _EXCEPTION; } }

        public Return(bool _success, object? _result = null, string? _message = null)
        {
            _SUCCESS = _success;
            _RESULT = _result;
            _MESSAGE = _message;

            if (_RESULT is Exception _ex)
            {
                _EXCEPTION = _ex;
                _MESSAGE ??= _ex.Message;
            }
        }
        public Return(Exception _exception)
        {
            _EXCEPTION = (Exception?)_exception;
        }

        public override string ToString()
        {
            return string.Format("Success: {0}; {1}", (Success == true ? "YES" : "no"), Message);
        }
    }
}
