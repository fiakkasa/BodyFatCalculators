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
            get => CultureInfo.CurrentCulture;
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
            Culture =
                await _js.InvokeAsync<string>("blazorCulture.get").ConfigureAwait(true) is string storedCulture
                    ? new CultureInfo(storedCulture)
                    : SupportedCultures[0];
        }
    }
}
