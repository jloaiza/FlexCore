﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreLogic.exceptions
{
    internal class InsertException:Exception
    {

        public InsertException()
        {
        }

        public InsertException(string message)
            : base(message)
        {
        }

        public InsertException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
