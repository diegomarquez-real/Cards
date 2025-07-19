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
                new NavItem { Id = "2", Href = "/yugioh", Text = "Yugioh", CustomIconName="bi-custom-base bi-yugioh-card-back" }
            };
            return navItems;
        }
    }
}