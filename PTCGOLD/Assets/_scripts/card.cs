using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public string _id;//ie BS042 or JU062 or FO001
    public string _kind;//pkmn; energy; trainer
    public List<Energy> GetEnergy()
    {
        return new List<Energy>();
    }
}
