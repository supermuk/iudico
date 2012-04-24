namespace IUDICO.Common.Models
{
    public class MenuItem
    {
        public string Text { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }

        public MenuItem(string text, string controller, string action)
        {
            this.Text = text;
            this.Controller = controller;
            this.Action = action;
        }
    }
}
