using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    List<Card> _deck = new List<Card>();
    CardLibrary cardlib = new CardLibrary();
    // Start is called before the first frame update
    void Start()
    {
        //Populate();
    }

    //This shuffle function taken from:https://stackoverflow.com/questions/273313/randomize-a-listt
    public void Shuffle()
    {
        Random rng = new Random();
        int n = _deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0,n + 1);
            Card value = _deck[k];
            _deck[k] = _deck[n];
            _deck[n] = value;
        }
    }

    void AddCard(Card card)
    {
        _deck.Add(card);
    }

    public void Populate()
    {
        for (int i = 0; i < 20; i++)
        {
            AddCard(new Energy("BS102", 'p'));
            AddCard(new Pokemon("Mewtwo", "PR003", 'p', 70, 2, 'p', 'n'));
            AddCard(new Pokemon("Clefairy", "BSXXX", 'p', 70, 2, 'p', 'n'));

            AddCard(cardlib.Switch());
        }
    }

    public int Size()
    {
        return _deck.Count;
    }

    public Card Draw()
    {
        Card card = _deck[0];
        _deck.RemoveAt(0);
        return card;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
