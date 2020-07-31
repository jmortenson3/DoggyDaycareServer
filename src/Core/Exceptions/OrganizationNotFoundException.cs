using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class OrganizationNotFoundException : Exception
    {
        public OrganizationNotFoundException(string message) : base(message)
        {
        }
    }
}
