﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models
{
    public class Action
    {
        public string Link { get; protected set; }
        public string Name { get; protected set; }

        public Action(string name, string link)
        {
            Name = name;
            Link = link;
        }
    }
}
