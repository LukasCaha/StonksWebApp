﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stonks.Models.Persons
{
    public class User : Person
    {
        public Portfolio userPortfolio { get; set; }
    }
}
