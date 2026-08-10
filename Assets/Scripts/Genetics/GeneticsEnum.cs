using System;

namespace FarmJam2026
{
    #region Color
    [Serializable]
    public enum EGeneColor
    {
        Red,
        Blue,
        Yellow,
    }
    [Serializable]
    public enum EGeneShade
    {
        Light,
        Medium,
        Dark,
    }
    [Serializable]
    public enum ColorName
    {
        Red,
        LightRed,
        DarkRed,
        Purple,
        LightPurple,
        DarkPurple,
        Blue,
        LightBlue,
        DarkBlue,
        Yellow,
        LightYellow,
        DarkYellow,
        Green,
        LightGreen,
        DarkGreen,
        Orange,
        LightOrange,
        DarkOrange
    }
    #endregion

    #region Variant
    [Serializable]
    public enum EBodyType
    {
        Smoky,
        Flatty,
        Hairy,
        Many,
        ENUM_COUNT,
    }
    #endregion
}
