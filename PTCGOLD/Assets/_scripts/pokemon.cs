using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : Card
{
    public string _species;//ie charmander
    //public string _id;//ie BS042 or JU062 or FO001
    public char _type;
    /*Types:
     * c = colorless
     * f = fighting
     * g = grass
     * w = water
     * l = lightning
     * p = psychic
     * r = fire
     * n = null (for instance, a card that has no weakness would have n for its weakness)
     */
    public int _max_hp;//ie 70
    public int _curr_hp;//ie 40, never above _max_hp

    public int _retreat;//0-4 inclusive
    public char _weakness;//ie p
    public char _resistance;//ie n

    public int _stage;//0,1,2;0->basic

    public List<Attack> _attacks;

    public List<Energy> _energy;

    public Pokemon(string species="", string id="BS000", char type='n', 
        int max_hp=40, int retreat=0, char weakness='n', char resistance='n',
        int stage=0,
        List<Attack> attacks=default(List<Attack>))
    {
        _kind = "pkmn";
        _species = species;
        _id = id;
        _type = type;
        _max_hp = max_hp;
        _curr_hp = max_hp;
        _retreat = retreat;
        _weakness = weakness;
        _resistance = resistance;
        _stage = stage;
        _attacks = attacks;
        _energy = new List<Energy>();
    }

    public void Attach_Energy(Energy energy)
    {
        _energy.Add(energy);
    }

    public string to_str()
    {
        string result = "";
        result = _species + " " + _curr_hp.ToString() + " " + _energy.Count.ToString();
        return result;
    }

    public List<Energy> GetEnergy()
    {
        return _energy;
    }
}
