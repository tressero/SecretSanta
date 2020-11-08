#pragma checksum "/home/mochsner/code/Blazor/SecretSanta/Pages/Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d9e8d8f92a8b15538d6cb877a8c172f0680bbe42"
// <auto-generated/>
#pragma warning disable 1591
namespace SecretSanta.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/home/mochsner/code/Blazor/SecretSanta/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/mochsner/code/Blazor/SecretSanta/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/mochsner/code/Blazor/SecretSanta/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/home/mochsner/code/Blazor/SecretSanta/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/home/mochsner/code/Blazor/SecretSanta/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/home/mochsner/code/Blazor/SecretSanta/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/home/mochsner/code/Blazor/SecretSanta/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/home/mochsner/code/Blazor/SecretSanta/_Imports.razor"
using SecretSanta;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/home/mochsner/code/Blazor/SecretSanta/_Imports.razor"
using SecretSanta.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/mochsner/code/Blazor/SecretSanta/Pages/Index.razor"
using SecretSanta.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/mochsner/code/Blazor/SecretSanta/Pages/Index.razor"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "h1");
            __builder.AddContent(1, "Hello ");
#nullable restore
#line 5 "/home/mochsner/code/Blazor/SecretSanta/Pages/Index.razor"
           if (User_ != null)
          {
              

#line default
#line hidden
#nullable disable
            __builder.AddContent(2, 
#nullable restore
#line 7 "/home/mochsner/code/Blazor/SecretSanta/Pages/Index.razor"
                User_.FirstName + " " + User_.LastName

#line default
#line hidden
#nullable disable
            );
#nullable restore
#line 7 "/home/mochsner/code/Blazor/SecretSanta/Pages/Index.razor"
                                                       
          }

#line default
#line hidden
#nullable disable
            __builder.AddContent(3, "!");
            __builder.CloseElement();
            __builder.AddMarkupContent(4, "\n\nWelcome to your Secret Santa Profile\n\n");
        }
        #pragma warning restore 1998
#nullable restore
#line 13 "/home/mochsner/code/Blazor/SecretSanta/Pages/Index.razor"
 
    User User_ = null;
    protected override async Task OnInitializedAsync()
    {
        var query = GetUser();
        query.Wait();
        User_ = query.Result;
    }

    private static async Task<User> GetUser()
    {
        User _user = null;

        using (var context = new SecretSantaContext())
        {
            _user = await (context.Users.Where(u => u.UserId == 1).FirstOrDefaultAsync<User>());
            // Guide for async LINQ: https://www.entityframeworktutorial.net/entityframework6/async-query-and-save.aspx
        }

        return _user;
    }
    
    private static async Task SaveUser(User editedUser)
    {
        using (var context = new SecretSantaContext())
        {
            context.Entry(editedUser).State = EntityState.Modified;
            int x = await (context.SaveChangesAsync());
        }
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
