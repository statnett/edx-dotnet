using System;

namespace Statnett.EdxLib.Exceptions
{
    public class EdxException : ApplicationException
    {
        public EdxException(string message) : base(message) {}

    }
}
