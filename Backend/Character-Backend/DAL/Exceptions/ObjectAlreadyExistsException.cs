using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException(string entity, string title) : base($"{entity} with {title} as identifier already exists in database") { }
    }
}

