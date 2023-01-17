/*
NET35
NET40
NET45
NETSTANDARD1_0
NETSTANDARD1_1
NETSTANDARD1_2
NETSTANDARD1_3
NETSTANDARD1_4
NETSTANDARD1_5
NETSTANDARD1_6
NETSTANDARD2_0
NETSTANDARD2_1
*/

using System;

namespace imL
{
    public class Return
    {
        readonly bool _SUCCESS;
        readonly object _RESULT;
        readonly bool _EXCEPTION;
        readonly string _MESSAGE;

        public bool Success { get { return _SUCCESS; } }
        public object Result { get { return _RESULT; } }
        public bool Exception { get { return _EXCEPTION; } }
        public string Message { get { return _MESSAGE; } }

        public Return(bool _success, object _result = null)
        {
            _SUCCESS = _success;
            _RESULT = _result;

            if (_RESULT is Exception _ex)
            {
                _SUCCESS = false;
                _EXCEPTION = true;
                _MESSAGE = _ex.Message;
            }

            if (_SUCCESS)
                _MESSAGE = "Undefined";
        }

        public override string ToString()
        {
            return string.Format("Success: {0}, {1}", (_SUCCESS ? "YES" : "NO"), _MESSAGE);
        }
    }
}
