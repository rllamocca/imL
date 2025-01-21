using System;

namespace imL.NC
{
    public static class ReturnExtension
    {
        public static void TriggerError(this Return _this)
        {
            if (_this.Success == false)
                throw new Exception(_this.Message ?? "empty message");
        }
        public static void TriggerException(this Return _this)
        {
            if (_this.Exception == null)
                return;

            throw _this.Exception;
        }
        public static void TriggerErrorException(this Return _this)
        {
            _this.TriggerError();
            _this.TriggerException();
        }

        public static void TriggerError(this Return[] _array)
        {
            if (_array == null)
                return;

            foreach (Return _item in _array)
                _item.TriggerError();
        }
        public static void TriggerException(this Return[] _array)
        {
            if (_array == null)
                return;

            foreach (Return _item in _array)
                _item.TriggerException();
        }
        public static void TriggerErrorException(this Return[] _array)
        {
            if (_array == null)
                return;

            foreach (Return _item in _array)
                _item.TriggerErrorException();
        }
    }
}
