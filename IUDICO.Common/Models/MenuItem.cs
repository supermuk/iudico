namespace IUDICO.Common.Models
{
    public class MenuItem
    {
        public string Text { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }

        public MenuItem(string text, string controller, string action)
        {
            Text = text;
            Controller = controller;
            Action = action;
        }
    }
}
