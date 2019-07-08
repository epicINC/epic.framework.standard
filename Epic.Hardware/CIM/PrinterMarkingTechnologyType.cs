using System;
using System.Collections.Generic;
using System.Text;

namespace Epic.Hardware.CIM
{
    public enum PrinterMarkingTechnologyType : ushort
    {
        Other = 1,
        Unknown = 2,
        ElectrophotographicLED = 3,
        ElectrophotographicLaser = 4,
        ElectrophotographicOther = 5,
        ImpactMovingHeadDotMatrix9pin = 6,
        ImpactMovingHeadDotMatrix24pin = 7,
        ImpactMovingHeadDotMatrixOther = 8,
        ImpactMovingHeadFullyFormed = 9,
        ImpactBand = 10,
        ImpactOther = 11,
        InkjetAqueous = 12,
        InkjetSolid = 13,
        InkjetOther = 14,
        Pen = 15,
        ThermalTransfer = 16,
        ThermalSensitive = 17,
        ThermalDiffusion = 18,
        ThermalOther = 19,
        Electroerosion = 20,
        Electrostatic = 21,
        PhotographicMicrofiche = 22,
        PhotographicImagesetter = 23,
        PhotographicOther = 24,
        IonDeposition = 25,
        eBeam = 26,
        Typesetter = 27,
    }
}
