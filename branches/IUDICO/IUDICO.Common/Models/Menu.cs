using System;
using System.Linq;
using System.Collections.Generic;

namespace IUDICO.Common.Models
{
    public class Menu
    {
        protected List<MenuItem> _Items;

        public IEnumerable<MenuItem> Items
        {
            get { return _Items; }
        }

        public IEnumerable<MenuItem> GetItems(Func<MenuItem, bool> predicate) 
        {
            return _Items.Where(predicate);
        }

        public Menu()
        {
            _Items = new List<MenuItem>();
        }

        public void Add(MenuItem item)
        {
            _Items.Add(item);
        }

        public void Add(IEnumerable<MenuItem> items)
        {
            _Items.AddRange(items);
        }
    }
}
