﻿namespace IUDICO.Common.Models
{
    public class Action
    {
        public string Link { get; protected set; }
        public string Name { get; protected set; }

        public Action(string name, string link)
        {
            this.Name = name;
            this.Link = link;
        }
    }
}
