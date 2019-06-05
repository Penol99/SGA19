using UnityEngine;
using System.Collections.Generic;

public class Scr_math_formulas
{


    public static float Pythagoras(float adj, float opp)
    {
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(adj, 2) + Mathf.Pow(opp, 2));

        return hypotenuse;
    }
    public static float SquareToCircle(float x, float y)
    {
        Vector2 circle = new Vector2(x * Mathf.Sqrt(1 - y * y * 0.5f),y * Mathf.Sqrt( 1 - x * x * 0.5f));
        return circle.x + circle.y;

    }
    public static float TwoAxisAngle(float x, float y)
    {
        float angle = Mathf.Atan2(x,y) * 180/Mathf.PI;

        return angle;
    }

}

