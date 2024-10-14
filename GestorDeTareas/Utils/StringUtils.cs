﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeTareas.Utils
{
    internal class StringUtils
    {
        public static int? ConvertToNumber(string str)
        {
            if (!int.TryParse(str, out int val))
            {
                return null;
            }
            return val;
        }
    }
}