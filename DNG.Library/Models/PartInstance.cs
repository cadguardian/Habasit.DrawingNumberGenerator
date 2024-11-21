using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNG.Library.Models;

public sealed record PartInstance(Part Part, int Length, bool IsCut);