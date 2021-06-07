// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace RemindMeOfSpecialBirthdays.Client.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using RemindMeOfSpecialBirthdays.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using RemindMeOfSpecialBirthdays.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using RemindMeOfSpecialBirthdays.Client.Components.SpecialBirthdaysTable;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using RemindMeOfSpecialBirthdays.Client.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using RemindMeOfSpecialBirthdays.Client.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using Entities.RequestFeatures;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\_Imports.razor"
using Markdig;

#line default
#line hidden
#nullable disable
    public partial class CopyToClipboardButton : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 10 "C:\Users\Hp\source\repos\RemindMeOfSpecialBirthdays\RemindMeOfSpecialBirthdays\Client\Shared\CopyToClipboardButton.razor"
      
    private const string _successButtonClass = "btn btn-success";
    private const string _infoButtonClass = "btn btn-info";
    private const string _errorButtonClass = "btn btn-danger";

    private const string _copyToClipboardText = "Copy";
    private const string _copiedToClipboardText = "Copied";
    private const string _errorText = "Oops. Try again.";

    private const string _fontAwesomeCopyClass = "fa fa-clipboard";
    private const string _fontAwesomeCopiedClass = "fa fa-check";
    private const string _fontAwesomeErrorClass = "fa fa-exclamation-circle";

    [Parameter]
    public string Text { get; set; }

    record ButtonData(bool IsDisabled, string ButtonText, string ButtonClass, string FontAwesomeClass);

    ButtonData buttonData = new(false, _copyToClipboardText, _infoButtonClass, _fontAwesomeCopyClass);

    public async Task CopyToClipboard()
    {
        var originalData = buttonData;
        try
        {
            await ClipboardService.WriteTextAsync(Text);

            buttonData = new ButtonData(true, _copiedToClipboardText,
                                        _successButtonClass, _fontAwesomeCopiedClass);
            await TriggerButtonState();
            buttonData = originalData;
        }
        catch
        {
            buttonData = new ButtonData(true, _errorText,
                                        _errorButtonClass, _fontAwesomeErrorClass);
            await TriggerButtonState();
            buttonData = originalData;
        }
    }

    private async Task TriggerButtonState()
    {
        StateHasChanged();
        await Task.Delay(TimeSpan.FromMilliseconds(1500));
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ClipboardService ClipboardService { get; set; }
    }
}
#pragma warning restore 1591
