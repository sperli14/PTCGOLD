using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLibrary
{
    //Trainers
    public Trainer Bill()
    {
        List<string> effects = new List<string>();
        effects.Add("dr2");
        return new Trainer("BS091","Bill",effects);
    }
    public Trainer ProfessorOak()
    {
        List<string> effects = new List<string>();
        effects.Add("discard");
        effects.Add("dr7");
        return new Trainer("BS088","ProfessorOak", effects);
    }
    public Trainer PlusPower()
    {
        List<string> effects = new List<string>();
        effects.Add("pp");
        return new Trainer("BS084","PlusPower", effects);
    }
    public Trainer GustOfWind()
    {
        List<string> effects = new List<string>();
        effects.Add("gust");
        return new Trainer("BS093", "Gust of Wind", effects);
    }
    public Trainer Switch()
    {
        List<string> effects = new List<string>();
        effects.Add("switch");
        return new Trainer("BS095", "Switch", effects);
    }

    /*
     *     public Attack(string name="", string cost="1c", int damage=0, bool coin=false, string fail_coin="",
        string success_coin="", string effect="")
    {
        _name = name;
        _cost = cost;
        _damage = damage;
        _coin = coin;
        _fail_coin = fail_coin;
        _success_coin = success_coin;
        _effect = effect;
    }
    
    public Pokemon(string species="", string id="BS000", char type='n', 
        int max_hp=40, int retreat=0, char weakness='n', char resistance='n',
        int stage=0,
        List<Attack> attacks=default(List<Attack>))
     */

    //Pokemon
    public Pokemon Mewtwo_promo()
    {
        List<Attack> attacks = new List<Attack>();
        attacks.Add(new Attack("Energy Absorption", "1p", effect: "pull2energy"));
        attacks.Add(new Attack("Psyburn", "2p1c", 40));
        return new Pokemon("Mewtwo", "PR003", 'p', 70, 2, 'p', 'n', 0, attacks);
    }
    public Pokemon Scyther_jungle()
    {
        List<Attack> attacks = new List<Attack>();
        attacks.Add(new Attack("Swords Dance", "1g", effect: "swordsdance"));
        attacks.Add(new Attack("Slash", "3c", 30));
        return new Pokemon("Scyther", "JN010", 'g', 70, 0, 'r', 'f', 0, attacks);
    }

    //Energy
    public Energy Psychic()
    {
        return new Energy("BS102", 'p');
    }
}
