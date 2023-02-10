using Microsoft.AspNetCore.Components;
using System.IO;

namespace LXMacros.Components
{
    public partial class MacroTree
    {
        private HashSet<string> isExpanded = new HashSet<string>();

        [Parameter]
        public string Path { get; set; }

        [Parameter]
        public IEnumerable<string> Macros { get; set; }

        private bool GetNextFolder(string rootPath, string path, out string title)
        {
            string relativePath = path.Substring(rootPath?.Length??0).TrimStart('.');

            var nextBreak = relativePath.IndexOf('.');
            title = nextBreak > 0 ? relativePath.Substring(0, relativePath.IndexOf('.')) : relativePath;
            return nextBreak < 0;
        }

        public IEnumerable<string> GetFolders()
        {
            if(Macros != null)
            {
                foreach(var macro in Macros)
                {
                    if ((Path == null || macro.StartsWith(Path)) && !GetNextFolder(Path, macro, out string folder))
                        yield return folder;
                }
            }
        }

        protected string GetTitle(string path)
        {
            return path.Substring(path.LastIndexOf('.')).Trim('.');
        }

        private string JoinPath(string rootPath, string folder)
        {
            if (string.IsNullOrEmpty(rootPath))
                return $"{folder}";
            return $"{Path}.{folder}";
        }

        public IEnumerable<string> GetMacrosInFolder(string folder)
        {
            return Macros.Where(item => item.StartsWith($"{JoinPath(Path,folder)}."));
        }

        public IEnumerable<string> GetItems()
        {
            return Macros.Where(item => GetNextFolder(Path, item, out string folder));
        }

        protected void ExpandFolder(string folder)
        {
            if (isExpanded.Contains(folder))
                isExpanded.Remove(folder);
            else
                isExpanded.Add(folder);
            StateHasChanged();
        }
    }
}
