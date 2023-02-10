using LXProtocols.AvolitesWebAPI;
using LXProtocols.AvolitesWebAPI.Blazor;
using Microsoft.AspNetCore.Components;

namespace LXMacros.Shared
{
    public partial class NavMenu
    {
        [Inject]
        public IAvolitesTitan Titan { get; set; }

        public IEnumerable<string> Macros { get; set; }

        private string searchQuery = string.Empty;

        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                if (searchQuery != value)
                {
                    searchQuery = value;
                    StateHasChanged();
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if(await Titan.IsConnected())
            {
                Macros = await Titan.API.Macros.GetMacroIds(true);
            }
            
            StateHasChanged();
        }

        private IEnumerable<string> FilterMacros()
        {
            if (string.IsNullOrEmpty(SearchQuery))
                return Macros;
            return Macros.Where(item => item.Contains(SearchQuery));
        }
    }
}
