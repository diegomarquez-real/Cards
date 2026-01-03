using BlazorBootstrap;

namespace Cards.BlazorServer.Components.Layout
{
    public partial class NavMenu
    {
        IEnumerable<NavItem>? navItems;

        private async Task<Sidebar2DataProviderResult> LoadNavMenu(Sidebar2DataProviderRequest request)
        {
            if (navItems?.Any() != true)
            {
                navItems = GetNavItems();
            }
            return await Task.FromResult(request.ApplyTo(navItems));
        }

        private IEnumerable<NavItem> GetNavItems()
        {
            navItems = new List<NavItem>
            {
                new NavItem { Id = "1", Href = "/", Text = "Home", IconName = IconName.HouseDoorFill },
                new NavItem { Id = "2", Text = "Yugioh" },
                new NavItem { Id = "3", ParentId = "2", Href = "/Yugioh/Database", Text = "Card Database", CustomIconName="bi-custom-base bi-yugioh-card-back" }
            };
            return navItems;
        }
    }
}