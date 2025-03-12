using DNG.Library.Models.Base;
using Microsoft.AspNetCore.Components.Web;

namespace DNG.Library.ViewModels
{
    internal class DWGNumberDecipherViewModel
    {
        public DWGNumberDecipherViewModel(IDrawingNumberDecipherService DecipherService)
        {
        }

        public IDrawingNumberDecipherService DecipherService;
        private string drawingNumberInput = string.Empty;
        private Dictionary<string, (string DrawingCode, string DrawingRequestValue)> decipheredResult;
        private bool isDeciphered = false;

        private void HandleDecipher()
        {
            if (!string.IsNullOrWhiteSpace(drawingNumberInput))
            {
                decipheredResult = DecipherService.DecipherDrawingNumber(drawingNumberInput);
                isDeciphered = true;
            }
        }

        private void ClearInput()
        {
            drawingNumberInput = string.Empty;
            decipheredResult = null;
            isDeciphered = false;
        }

        private async Task HandleKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                HandleDecipher();
            }
        }
    }
}