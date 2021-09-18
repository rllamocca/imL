using System;
using System.Net.Mail;

namespace imL.Utility.Mail
{
    public static class String_imlUtilityMailExtension
    {
        public static bool IsMail(this string _this, bool _throw = false)
        {
            if (string.IsNullOrEmpty(_this))
                return false;

            try
            {
                new MailAddress(_this);

                return true;
            }
            catch (Exception _ex)
            {
                if (_throw)
                    throw _ex;
            }

            return false;
        }
    }
}
