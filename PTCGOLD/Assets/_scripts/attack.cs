using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{

    public string _name;
    public string _cost;//ie 2w1c for 2 water and 1 colorless
    public int _damage;//the base attack value of the attack
    public bool _coin;//do you flip a coin for this attack?
    public string _fail_coin;//what happens if you fail the flip. "fail" for attack does nothing; "-xx" where xx is the damage to self
    public string _success_coin;//what happens if you succeed the flip. Use a status effect code for status effects; "+xx" for bonus damage
    public string _effect;//guaranteed effects.
    /*Status codes:
     * asl = asleep
     * psn = poison
     * con = confusion
     * par = paralyzed
     */

    public Attack(string name="", string cost="1c", int damage=0, bool coin=false, string fail_coin="",
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
}
