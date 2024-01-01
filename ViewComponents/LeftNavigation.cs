using LAB6.ViewComponents;
using Microsoft.AspNetCore.Mvc;
public class LeftNavigation : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var links = new List<NavLink>
        {
            new NavLink("Home", "Index","Home"),
            new NavLink("Employee", "Index","Employees"),
            new NavLink("Home", "Privacy","Privacy"),

        };

        return View(links);
    }
}
