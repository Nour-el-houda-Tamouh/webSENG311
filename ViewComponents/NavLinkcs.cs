namespace LAB6.ViewComponents
{
    public class NavLink
    {
        public NavLink(string controller, string action, string text)
        {
            Controller = controller;
            Action = action;
            Text = text;
        }

        public string Controller { get; }
        public string Action { get; }
        public string Text { get; }
    }

}
