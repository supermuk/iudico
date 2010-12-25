using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models
{
    public class Menu
    {
        public List<MenuItem> Items { get; private set; }

        public Menu()
        {
            this.Items = new List<MenuItem>();
        }

        public void Add(MenuItem item)
        {
            Items.Add(item);
        }
    }
}
