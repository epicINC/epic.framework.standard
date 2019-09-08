using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using Epic.Hardware.Extensions;

namespace Epic.Hardware.CIM
{
    public class Printer : LogicalDevice
    {
        internal Printer()
        {

        }


        protected Printer(ManagementBaseObject value) : base(value)
        {
            Parse(this, value);
        }

        protected static void Parse(Printer instance, ManagementBaseObject value)
        {
            instance.AvailableJobSheets = value["AvailableJobSheets"].TryCast<string[]>();
            instance.Capabilities = value["Capabilities"].TryCast<ushort, PrinterCapabilityType>();
            instance.CapabilityDescriptions = value["CapabilityDescriptions"].TryCast<string[]>();
            instance.CharSetsSupported = value["CharSetsSupported"].TryCast<string[]>();
            instance.CurrentCapabilities = value["CurrentCapabilities"].Cast<PrinterCapabilityType>();
            instance.CurrentCharSet = value["CurrentCharSet"].TryCast<string>();
            instance.CurrentLanguage = value["CurrentLanguage"].Cast<PrinterLanguageType>();
            instance.CurrentMimeType = value["CurrentMimeType"].TryCast<string>();
            instance.CurrentNaturalLanguage = value["CurrentNaturalLanguage"].TryCast<string>();
            instance.CurrentPaperType = value["CurrentPaperType"].TryCast<string>();
            instance.DefaultCapabilities = value["DefaultCapabilities"].Cast<PrinterCapabilityType>();
            instance.DefaultCopies = value["DefaultCopies"].Cast<uint>();
            instance.DefaultLanguage = value["DefaultLanguage"].Cast<PrinterLanguageType>();
            instance.DefaultMimeType = value["DefaultMimeType"].TryCast<string>();
            instance.DefaultNumberUp = value["DefaultNumberUp"].Cast<uint>();
            instance.DefaultPaperType = value["DefaultPaperType"].TryCast<string>();
            instance.DetectedErrorState = value["DetectedErrorState"].Cast<PrinterDetectedErrorStateType>();
            instance.ErrorInformation = value["ErrorInformation"].TryCast<string[]>();
            instance.HorizontalResolution = value["HorizontalResolution"].Cast<uint>();
            instance.JobCountSinceLastReset = value["JobCountSinceLastReset"].Cast<uint>();
            instance.LanguagesSupported = value["LanguagesSupported"].TryCast<ushort, PrinterLanguageType>();
            instance.MarkingTechnology = value["MarkingTechnology"].Cast<PrinterMarkingTechnologyType>();
            instance.MaxCopies = value["MaxCopies"].Cast<uint>();
            instance.MaxNumberUp = value["MaxNumberUp"].Cast<uint>();
            instance.MaxSizeSupported = value["MaxSizeSupported"].Cast<uint>();
            instance.MimeTypesSupported = value["MimeTypesSupported"].TryCast<string[]>();
            instance.NaturalLanguagesSupported = value["NaturalLanguagesSupported"].TryCast<string[]>();
            instance.PaperSizesSupported = value["PaperSizesSupported"].TryCast<ushort, PrinterPaperSizeType>();
            instance.PaperTypesAvailable = value["PaperTypesAvailable"].TryCast<string[]>();
            instance.PrinterStatus = (PrinterStatusType)value["PrinterStatus"];
            instance.TimeOfLastReset = value["TimeOfLastReset"].Cast<DateTime>();
            instance.VerticalResolution = value["VerticalResolution"].Cast<uint>();
        }

        public string[] AvailableJobSheets { get; protected set; }
        public PrinterCapabilityType[] Capabilities { get; protected set; }
        public string[] CapabilityDescriptions { get; protected set; }
        public string[] CharSetsSupported { get; protected set; }
        public PrinterCapabilityType? CurrentCapabilities { get; protected set; }
        public string CurrentCharSet { get; protected set; }
        public PrinterLanguageType? CurrentLanguage { get; protected set; }
        public string CurrentMimeType { get; protected set; }
        public string CurrentNaturalLanguage { get; protected set; }
        public string CurrentPaperType { get; protected set; }
        public PrinterCapabilityType? DefaultCapabilities { get; protected set; }
        public uint? DefaultCopies { get; protected set; }
        public PrinterDetectedErrorStateType? DetectedErrorState { get; protected set; }
        public PrinterLanguageType? DefaultLanguage { get; protected set; }
        public string DefaultMimeType { get; protected set; }
        public uint? DefaultNumberUp { get; protected set; }
        public string DefaultPaperType { get; protected set; }
        public string[] ErrorInformation { get; protected set; }
        public uint? HorizontalResolution { get; protected set; }
        public uint? JobCountSinceLastReset { get; protected set; }
        public PrinterLanguageType[] LanguagesSupported { get; protected set; }
        public PrinterMarkingTechnologyType? MarkingTechnology { get; protected set; }
        public uint? MaxCopies { get; protected set; }
        public uint? MaxNumberUp { get; protected set; }
        public uint? MaxSizeSupported { get; protected set; }
        public string[] MimeTypesSupported { get; protected set; }
        public string[] NaturalLanguagesSupported { get; protected set; }
        public PrinterPaperSizeType[] PaperSizesSupported { get; protected set; }
        public string[] PaperTypesAvailable { get; protected set; }
        public PrinterStatusType? PrinterStatus { get; protected set; }
        public DateTime? TimeOfLastReset { get; protected set; }
        public uint? VerticalResolution { get; protected set; }
    }
}
