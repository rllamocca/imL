using System;

namespace imL
{
    public static class ReturnExtension
    {
        public static void TriggerError(this Return _this)
        {
            if (_this.Success == false)
                throw new Exception(_this.Message);
        }
        public static void TriggerException(this Return _this)
        {
            if (_this.Exception)
                throw (Exception)_this.Result;
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

            for (int _i = 0; _i < _array.Length; _i++)
                _array[_i].TriggerError();
        }
        public static void TriggerException(this Return[] _array)
        {
            if (_array == null)
                return;

            for (int _i = 0; _i < _array.Length; _i++)
                _array[_i].TriggerException();
        }
        public static void TriggerErrorException(this Return[] _array)
        {
            if (_array == null)
                return;

            for (int _i = 0; _i < _array.Length; _i++)
                _array[_i].TriggerErrorException();
        }

        public static bool Success(this Return[] _array)
        {
            if (_array == null)
                return false;

            bool _return = true;

            for (int _i = 0; _i < _array.Length; _i++)
            {
                Return _item = _array[_i];
                _return = _return && _item.Success;
            }

            return _return;
        }
        public static bool Exception(this Return[] _array)
        {
            if (_array == null)
                return false;

            bool _return = false;

            for (int _i = 0; _i < _array.Length; _i++)
            {
                Return _item = _array[_i];
                _return = _return || _item.Exception;
            }

            return _return;
        }
        public static bool ErrorException(this Return[] _array)
        {
            if (_array == null)
                return false;

            bool _return = false;

            for (int _i = 0; _i < _array.Length; _i++)
            {
                Return _item = _array[_i];
                _return = _return || (_item.Success == false) || (_item.Exception == true);
            }

            return _return;
        }
    }
}
