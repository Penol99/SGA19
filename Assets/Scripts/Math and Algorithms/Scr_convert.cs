using System.Collections;
using System.Collections.Generic;


public class Scr_convert 
{
    public static bool ToBool(float value)
    {
        return value > 0;
    }
    public static bool ToBool(int value)
    {
        return value > 0;
    }
    public static int ToInt(bool value)
    {
        return value ? 1 : 0;
    }
}
