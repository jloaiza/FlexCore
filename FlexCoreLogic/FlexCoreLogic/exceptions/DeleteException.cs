using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreLogic.exceptions
{
    internal class DeleteException:Exception
    {
        public DeleteException()
        {
        }

        public DeleteException(string message)
            : base(message)
        {
        }

        public DeleteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
