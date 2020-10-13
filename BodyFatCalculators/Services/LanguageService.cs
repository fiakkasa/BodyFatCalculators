using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BodyFatCalculators.Services
{
    public class LanguageService
    {
        private readonly IJSRuntime _js;

        public List<CultureInfo> SupportedCultures { get; } =
          new List<CultureInfo>
          {
                new CultureInfo("en-US"),
                new CultureInfo("el-GR")
          };

        public CultureInfo Culture
        {
            get => CultureInfo.DefaultThreadCurrentCulture;
            set
            {
                var culture =
                    SupportedCultures.Any(x => x.Name == value.Name)
                        ? value
                        : SupportedCultures[0];

                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;

                if (_js is IJSInProcessRuntime jsRuntime && jsRuntime.Invoke<string>("blazorCulture.get") != culture.Name)
                    jsRuntime.InvokeVoid("blazorCulture.set", culture.Name);

                OnCultureChange?.Invoke();
            }
        }

        public event Action? OnCultureChange;

        public LanguageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task Init()
        {

#pragma warning disable S1135 // Track uses of "TODO" tags
#pragma warning disable S125 // Sections of code should not be commented out
            // todo: restore
            //Culture = 
            //    await _js.InvokeAsync<string>("blazorCulture.get").ConfigureAwait(true) is string storedCulture
            //        ? new CultureInfo(storedCulture)

            //        : SupportedCultures[0];
#pragma warning restore S125 // Sections of code should not be commented out

            // todo: remove
            // workaround for loading secondary language
            var culture =
                await _js.InvokeAsync<string>("blazorCulture.get").ConfigureAwait(true) is string storedCulture
                    ? new CultureInfo(storedCulture)
                    : SupportedCultures[0];

            var tempCulture = SupportedCultures.Last();

            CultureInfo.DefaultThreadCurrentCulture = tempCulture;
            CultureInfo.DefaultThreadCurrentUICulture = tempCulture;

            if (tempCulture.Name != culture.Name)
                _ = Task.Delay(100).ContinueWith((_) => Culture = culture);
            ///////////////////////////////////////////////////////////////
#pragma warning restore S1135 // Track uses of "TODO" tags
        }
    }
}
