using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Card
{
    public char _type;
    /*Types:
     * c = colorless (double colorless)
     * f = fighting
     * g = grass
     * w = water
     * l = lightning
     * p = psychic
     * r = fire
     * n = null (default value)
     */
     
   public Energy(string id = "BS000", char type = 'n')
    {
        _kind = "energy";
        _id = id;
        _type = type;
    }
}
