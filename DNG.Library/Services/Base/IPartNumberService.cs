﻿using DNG.Library.Models;
using DNG.Library.Models.Base;

namespace DNG.Library.Services.Base;

public interface IPartNumberService
{
    IEnumerable<KeyValuePair<string, string>> FilterPartNumbers(IDrawingRequest drawingRequest, string universalSearch = "", bool isFreeSearchMode = false);

    List<PartNumber> GetParts();

    Task InitializeAsync();
}