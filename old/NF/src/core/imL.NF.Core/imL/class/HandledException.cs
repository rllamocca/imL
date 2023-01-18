using System;
using System.Runtime.Serialization;

namespace imL
{
    [Serializable()]
    public class HandledException : Exception
    {
        protected HandledException() : base() { }
        protected HandledException(SerializationInfo _info, StreamingContext _context) : base(_info, _context) { }

        public HandledException(string _message) : base(_message) { }
        public HandledException(string _message, Exception _inner) : base(_message, _inner) { }

        //################################################################
        readonly string _CODE_;
        public string Code { get { return _CODE_; } }

        public HandledException(string _code, string _message) : base(_message) { _CODE_ = _code; }
        public HandledException(string _code, string _message, Exception _inner) : base(_message, _inner) { _CODE_ = _code; }
    }
}