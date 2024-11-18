using DNG.Library.Models.BeltSpecs;
using System;
using System.Collections.Generic;

namespace DNG.Library.Models
{
    public class DrawingNumberAttributeMetadata
    {
        public static List<(string AttributeName, int MaxLength, Func<string, string> LookupFunc)> GetAttributeMetadata()
        {
            return new List<(string AttributeName, int MaxLength, Func<string, string> LookupFunc)>
            {
                ("BeltType", 1, code => Lookup(BeltType.Options, code)),
                ("BeltSeries", 8, code => Lookup(BeltSeries.Options, code)),
                ("Color", 1, code => Lookup(MaterialColor.Options, code)),
                ("Material", 1, code => Lookup(BeltMaterial.Options, code)),
                ("AdderMaterial", 1, code => Lookup(AdderMaterial.Options, code)),
                ("RodMaterial", 1, code => Lookup(RodMaterial.Options, code)),
                ("BeltWidth", 4, ParseBeltWidth),
                ("FlightsRollersGrip", 2, code => Lookup(Flights_Rollers_Grips.Options, code)),
                ("QtyRollersAcrossWidth", 2, code => ParseQtyRollersAcrossWidth(code)),
                ("FRGCenters", 3, code => ParseFRGCenters(code)),
                ("BeltAccessories", 2, code => Lookup(BeltAccessories.Options, code)),
                ("FrictionAntiStatic", 1, code => Lookup(FrictionAntiStatic.Options, code)),
                ("SidePLLaneDV", 1, code => Lookup(SidePLLaneDV.Options, code)),
                ("UniqueIdentification", 1, code => code), // Direct mapping
                ("IndentCode", 2, code => Lookup(IndentCode.Options, code))
            };
        }

        /// <summary>
        /// Generic lookup function for dictionary-based options.
        /// </summary>
        private static string Lookup(Dictionary<string, string> options, string code)
        {
            return options.TryGetValue(code, out var value) ? value : "Unknown";
        }

        /// <summary>
        /// Parses the belt width from a 4-character code.
        /// </summary>
        private static string ParseBeltWidth(string code)
        {
            if (decimal.TryParse(code, out var width))
            {
                return BeltWidth.Create(width / 10).GetBeltWidth();
            }
            return "Invalid Width";
        }

        /// <summary>
        /// Parses the quantity of rollers across the width.
        /// </summary>
        private static string ParseQtyRollersAcrossWidth(string code)
        {
            if (int.TryParse(code, out var quantity))
            {
                return Flights_Rollers_Grips.GetFRGQuantityAcrossWidthCode(quantity);
            }
            return "Invalid Quantity";
        }

        /// <summary>
        /// Parses the FRG centers code into a human-readable format.
        /// </summary>
        private static string ParseFRGCenters(string code)
        {
            if (decimal.TryParse(code, out var dimension))
            {
                return Flights_Rollers_Grips.GetFRGCentersCode(dimension / 10);
            }
            return "Invalid FRG Centers";
        }
    }
}