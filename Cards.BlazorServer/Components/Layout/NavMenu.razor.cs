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
                new NavItem { Id = "1", Href = "/", IconName = IconName.HouseDoorFill, Text = "Home", },
                new NavItem { Id = "2", Href = "/yugioh", IconName = IconName.CardImage, Text = "Yugioh", }
            };
            return navItems;
        }
    }
}