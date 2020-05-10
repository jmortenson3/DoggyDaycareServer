using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class MultipleUsersWithSameEmailException : Exception
    {
        public MultipleUsersWithSameEmailException(string message) : base(message)
        {
        }
    }
}
