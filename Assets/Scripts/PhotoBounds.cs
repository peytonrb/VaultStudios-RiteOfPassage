using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoBounds : MonoBehaviour
{
    public float x1;
    public float x2;
    public float y1;
    public float y2;
    public bool xIn = false;
    public bool yIn = false;
    
    public bool inRange(float x, float y)
    {
        if (x1 < x2)
        {
            if (x > x1 && x < x2)
            {
                xIn = true;
            }
            else
            {
                xIn = false;
            }
        }
        else if (x1 > x2)
        {
            if (x > x1 || x < x2)
            {
                xIn = true;
            }
            else
            {
                xIn = false;
            }
        }

        if (y1 < y2)
        {
            if (y > y1 && y < y2)
            {
                yIn = true;
            }
            else
            {
                yIn = false;
            }
        }
        else if (y1 > y2)
        {
            if (y > y1 || y < y2)
            {
                yIn = true;
            }
            else
            {
                yIn = false;
            }
        }
        else
        {
            yIn = false;
        }

        if (xIn && yIn)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
