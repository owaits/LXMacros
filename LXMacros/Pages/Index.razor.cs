using BlazorMonaco;
using BlazorMonaco.Editor;
using LXProtocols.AvolitesWebAPI.Blazor;
using Microsoft.AspNetCore.Components;

namespace LXMacros.Pages
{
    public partial class Index
    {
        private MacroEditor _macroEditor = null;

        private StandaloneCodeEditor _codeEditor = null;

        public StandaloneCodeEditor CodeEditor
        {
            get { return _codeEditor; }
            set
            {
                if (_codeEditor != value)
                {
                    _codeEditor = value;
                    //_codeEditor?.SetValue(_macroEditor?.MacroScript);
                }
            }
        }

        private string macroId = null;

        [Parameter]
        public string MacroId 
        {
            get { return macroId; }
            set
            {
                if(macroId != value)
                {
                    macroId = value;
                    StateHasChanged();
                }
            }
        }

        private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "xml",
                Value = _macroEditor?.MacroScript
            };
        }

        private string lastCode = null;

        private async Task OnCodeChanged(ModelContentChangedEvent args)
        {
            if(_macroEditor != null)
            {
                var newCode = await _codeEditor.GetValue();

                if(newCode != lastCode)
                {
                    _macroEditor.MacroScript = await _codeEditor.GetValue();
                    lastCode = newCode;
                }                
            }            
        }
    }
}
