using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : Card
{
    public string _name;
    public List<string> _effects;
    //effects:
    /*
     * drX - draw X cards
     * discard - discard own hand
     * gust - gust enemy mon out
     * switch - switch own mon out
     * pp - increase attack power this turn by 10
     */
    public Trainer(string id = "BS000", string name="", List<string> effects= default(List<string>))
    {
        _kind = "trainer";
        _id = id;
        _name = name;
        _effects = effects;
    }
}
