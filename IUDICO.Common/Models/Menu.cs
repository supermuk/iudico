using System.Collections.Generic;

namespace IUDICO.Common.Models
{
    public class Menu
    {
        public List<MenuItem> Items { get; private set; }

        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public void Add(MenuItem item)
        {
            Items.Add(item);
        }
    }
}
