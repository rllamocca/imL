﻿using System;

namespace imL.Utility
{
    public static class PrimitiveExtension
    {
        public static DateTime TimeStampToDateTime(this double _this)
        {
            return ReadOnly._TIMESTAMP.AddSeconds(_this);
        }
    }
}