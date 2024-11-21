using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNG.Library.Models;

public sealed record Part(string Name, int Length, PartType Type);