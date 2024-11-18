using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNG.Library.Models.Base
{
    public interface IDrawingNumberDecipherService
    {
        Dictionary<string, (string DrawingCode, string DrawingRequestValue)> DecipherDrawingNumber(string drawingNumber);
    }
}