using System;
using System.Linq;
using System.Collections.Generic;

namespace IUDICO.Common.Models
{
    public class Menu
    {
        protected List<MenuItem> items;

        public IEnumerable<MenuItem> Items
        {
            get { return this.items; }
        }

        public IEnumerable<MenuItem> GetItems(Func<MenuItem, bool> predicate) 
        {
            return this.items.Where(predicate);
        }

        public Menu()
        {
            this.items = new List<MenuItem>();
        }

        public void Add(MenuItem item)
        {
            this.items.Add(item);
        }

        public void Add(IEnumerable<MenuItem> items)
        {
            this.items.AddRange(items);
        }
    }
}
