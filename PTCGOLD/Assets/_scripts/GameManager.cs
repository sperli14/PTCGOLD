using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject card_preview;
    [SerializeField]
    GameObject generic_card;
    bool turn = true;//is it p1's turn?
    bool energy_attach=false;//did the turn player attach an energy this turn?
    Pokemon test_poke;
    Attack test_attack;
    int cursorx = 0;
    int cursory = 0;//0=hand,1=bench,2=active
    int pp = 0;//how much additional damage this turn

    //Memory variables allow the user to click a card and then click another card to affect it
    //ie select energy -> select benched pokemon to attach to
    int memory_index;
    string memory_string;

    Pokemon p1_active_pkmn = null;
    Pokemon p2_active_pkmn = null;

    Deck p1_deck;
    Deck p2_deck;
    List<Card> p1_hand;
    List<Card> p2_hand;

    List<Card> p1_discard;
    List<Card> p2_discard;

    List<Card> p1_prizes;
    List<Card> p2_prizes;

    List<Pokemon> p1_bench;
    List<Pokemon> p2_bench;

    [SerializeField]
    GameObject p1_bench1;
    [SerializeField]
    GameObject p1_bench2;
    [SerializeField]
    GameObject p1_bench3;
    [SerializeField]
    GameObject p1_bench4;
    [SerializeField]
    GameObject p1_bench5;
    [SerializeField]
    GameObject p1_active;
    [SerializeField]
    GameObject p1_hand_start;
    [SerializeField]
    GameObject cursor_object;

    List<GameObject> p1_hand_display;

    // Start is called before the first frame update
    void Start()
    {
        CardLibrary cardlib = new CardLibrary();


        test_poke = cardlib.Mewtwo_promo();//new Pokemon("Mewtwo", "PR003", 'p', 70, 2, 'p', 'n', 0);
        Pokemon test_enemy_poke = cardlib.Mewtwo_promo();//new Pokemon("Mewtwo", "PR003", 'p', 70, 2, 'p', 'n', 0);
        p1_active_pkmn = test_poke;
        p1_active_pkmn._energy.Add(new Energy());
        p1_active_pkmn._energy.Add(new Energy());
        p1_active_pkmn._energy.Add(new Energy());
        p2_active_pkmn = test_enemy_poke;
        test_attack = new Attack("Psyburn", "2p1c", 40);

        p1_deck = new Deck();
        p1_deck.Populate();
        p1_deck.Shuffle();
        p1_hand = new List<Card>();
        p1_discard = new List<Card>();
        p1_prizes = new List<Card>();
        p1_bench = new List<Pokemon>();

        p2_deck = new Deck();
        p2_deck.Populate();
        p2_deck.Shuffle();
        p2_hand = new List<Card>();
        p2_discard = new List<Card>();
        p2_prizes = new List<Card>();
        p2_bench = new List<Pokemon>();

        p1_hand_display = new List<GameObject>();

        //random basic from deck to active

        for(int i=0;i<6;i++)
            p1_hand.Add(p1_deck.Draw());
        for (int i = 0; i < 6; i++)
            p1_prizes.Add(p1_deck.Draw());

        for (int i = 0; i < 6; i++)
            p2_hand.Add(p2_deck.Draw());
        for (int i = 0; i < 6; i++)
            p2_prizes.Add(p2_deck.Draw());


        DrawScreen();


        //print(player_hand[0]);

        
        //card_preview.GetComponent<CardPreview>().ChangeSprite(player_hand[cursor]._id);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (cursory == 0)//play card from hand
            {
                //PerformAttack(test_poke, test_attack);
                if (p1_hand[cursorx]._kind == "pkmn")
                {
                    bool result = PlayPokemon((Pokemon)p1_hand[cursorx]);
                    if (result)
                    {
                        p1_hand.RemoveAt(cursorx);
                        if(cursorx >= p1_hand.Count)
                            cursorx--;
                    }
                    DrawScreen();
                }
                if (p1_hand[cursorx]._kind == "energy")
                {
                    bool result = PlayEnergy((Energy)p1_hand[cursorx], 0);//////////////////////////////////////////////////////////////////////////
                    if (result)
                    {
                        p1_hand.RemoveAt(cursorx);
                        if (cursorx >= p1_hand.Count)
                            cursorx--;
                    }
                    DrawScreen();
                }
                if (p1_hand[cursorx]._kind == "trainer")
                {
                    bool result = PlayTrainer((Trainer)p1_hand[cursorx]);
                    if (result)
                    {
                        p1_hand.RemoveAt(cursorx);
                        if (cursorx >= p1_hand.Count)
                            cursorx--;
                    }
                    DrawScreen();
                }
            }
            else if(cursory == 2)//select active
            {

            }
            
        }

        if (Input.GetKeyDown("a"))
        {
            if (cursorx > 0)
            {
                cursorx--;
                DrawScreen();
            }
            //print(p1_hand[cursorx]._id);
        }
        if (Input.GetKeyDown("d"))
        {
            if (cursory == 0)
            {
                if (cursorx < p1_hand.Count - 1)
                {
                    cursorx++;
                    DrawScreen();
                }
            }
            else
            {
                if (cursorx < 4)
                {
                    cursorx++;
                    DrawScreen();
                }
            }
            //print(p1_hand[cursorx]._id);
        }
        if (Input.GetKeyDown("w"))
        {
            if (cursory < 2)
            {
                cursory++;
                cursorx = 0;
                DrawScreen();
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if (cursory > 0)
            {
                cursory--;
                cursorx = 0;
                DrawScreen();
            }
        }
        if (Input.GetKeyDown("r"))
        {
            Retreat(0);
            DrawScreen();
        }
        if (Input.GetKeyDown("z"))
        {
            PerformAttack(p1_active_pkmn, test_attack);//p1_active_pkmn._attacks[1]);
            DrawScreen();
        }
    }

    void DrawScreen()
    {
        //print();
        string output = "";
        output += p1_active_pkmn.to_str() + "\n";
        for (int i = 0; i < p1_bench.Count; i++)
            output = output + p1_bench[i].to_str() + "\n";
       // print(output);


        if (cursory == 0)
            card_preview.GetComponent<CardPreview>().ChangeSprite(p1_hand[cursorx]._id);
        else if (cursory == 1 && p1_bench.Count > cursorx)
            card_preview.GetComponent<CardPreview>().ChangeSprite(p1_bench[cursorx]._id);
        else if (p1_active_pkmn != null)
            card_preview.GetComponent<CardPreview>().ChangeSprite(p1_active_pkmn._id);
        if (p1_active_pkmn != null)
            p1_active.GetComponent<CardPreview>().ChangeSprite(p1_active_pkmn._id);
        if(p1_bench.Count > 0)
            p1_bench1.GetComponent<CardPreview>().ChangeSprite(p1_bench[0]._id);
        if (p1_bench.Count > 1)
            p1_bench2.GetComponent<CardPreview>().ChangeSprite(p1_bench[1]._id);
        if (p1_bench.Count > 2)
            p1_bench3.GetComponent<CardPreview>().ChangeSprite(p1_bench[2]._id);
        if (p1_bench.Count > 3)
            p1_bench4.GetComponent<CardPreview>().ChangeSprite(p1_bench[3]._id);
        if (p1_bench.Count > 4)
            p1_bench5.GetComponent<CardPreview>().ChangeSprite(p1_bench[4]._id);
        for (int i = 0; i < p1_hand_display.Count; i++)
            Destroy(p1_hand_display[i]);
        p1_hand_display.Clear();
        p1_hand_start.transform.position = new Vector3(-6,-4,-5);

        if(cursory == 0)//hand
            cursor_object.transform.position = new Vector3(-6 + (2 * cursorx), -4, -5);
        else if(cursory == 1)//bench
            cursor_object.transform.position = new Vector3(-2.3f + (1.15f * cursorx), -2f, -5);
        else//active
            cursor_object.transform.position = new Vector3(0, -1.62f+1f, -5);

        for (int i=0;i<p1_hand.Count;i++)
        {
            GameObject temp = Instantiate(generic_card, p1_hand_start.transform.position, p1_hand_start.transform.rotation);
            p1_hand_display.Add(temp);
            p1_hand_start.transform.position += new Vector3(2f, 0, 0);
            p1_hand_display[i].transform.GetChild(0).GetComponent<CardPreview>().ChangeSprite(p1_hand[i]._id);
        }
        
    }


    void ChangeTurns()
    {
        turn = !turn;
        pp = 0;
        if(turn)
        {
            if (p1_deck.Size() == 0)
                Win(!turn);//decking out
            else
                p1_hand.Add(p1_deck.Draw()); 
        }
        else
        {
            if (p2_deck.Size() == 0)
                Win(!turn);//decking out
            else
                p2_hand.Add(p2_deck.Draw());
        }
        //DO: turn player draws a card
        energy_attach = false;
    }

    void PerformAttack(Pokemon pokemon, Attack atk)
    {
        print(pokemon._species + " used " + atk._name + "!");
        TakeDamage(atk._damage, !turn);
    }

    void TakeDamage(int amount, bool which_active)
    {
        //which_active = true -> attacks the player's active pokemon
        
        if (which_active == false)
        {
            char type_attacking = p1_active_pkmn._type;
            char resistance = p2_active_pkmn._resistance;
            char weakness = p2_active_pkmn._weakness;
            if (type_attacking == weakness)
                amount = amount * 2;
            if (type_attacking == resistance)
                amount -= 30;
            p2_active_pkmn._curr_hp -= amount;
            if (p2_active_pkmn._curr_hp <= 0)
                Knockout(false);
            //print(amount);
        }
    }
    bool PlayPokemon(Pokemon pkmn)
    {
        if(turn)
        {
            if (pkmn._stage == 0)
            {
                if (p1_bench.Count < 5)
                {
                    p1_bench.Add(pkmn);
                    return true;
                }
            }
        }
        return false;
    }
    bool PlayEnergy(Energy energy, int index)
    {
        if (energy_attach)
            return false;
        if (turn)
        {
            if (index < 5)
            {
                p1_bench[index]._energy.Add(energy);
            }
            else
            {
                p1_active_pkmn._energy.Add(energy);
            }
            energy_attach = true;
            return true;
        }
        else
        {
            if (index < 5)
            {
                p2_bench[index]._energy.Add(energy);
            }
            else
            {
                p2_active_pkmn._energy.Add(energy);
            }
            energy_attach = true;
            return true;
        }
    }
    void Knockout(bool which_pkmn)
    {
        //which_pkmn = true -> p1 was knocked out
        if(which_pkmn)
        {
            Discard(p1_active_pkmn, which_pkmn);
            if (p1_bench.Count == 0)
            {
                Win(!which_pkmn);
                return;
            }

        }
        else
        {
            Discard(p2_active_pkmn, which_pkmn);
            if (p2_bench.Count == 0)
            {
                Win(!which_pkmn);
                return;
            }
        }
        DrawPrize(!which_pkmn);
        Promote(which_pkmn, AskForPkmn("Promote"));
    }

    bool Retreat(int index)
    {
        //index -> who we're switching to
        //Pokemon retreating = null;
        if(turn)
        {
            if(p1_active_pkmn._energy.Count >= p1_active_pkmn._retreat)
            {
                for (int i = 0; i < p1_active_pkmn._retreat; i++)
                {
                    Discard(p1_active_pkmn._energy[0], turn);
                    p1_active_pkmn._energy.RemoveAt(0);
                }
                Pokemon temp = p1_bench[index];
                p1_bench[index] = p1_active_pkmn;
                p1_active_pkmn = temp;
            }
            
        }
        DrawScreen();
        return true;
    }

    void Discard(Card card, bool which_player)
    {
        if(which_player)//player 1
        {
            if(card._kind=="pkmn")
            {
                List<Energy> energies = card.GetEnergy();
                for(int i=0;i<energies.Count;i++)
                {
                    Discard(energies[i], which_player);
                }
            }
            p1_discard.Add(card);
        }
        else
            p2_discard.Add(card);
    }

    int AskForPkmn(string message)
    {
        return 0;//Call appropriate functions on Bots
    }
    void DrawPrize(bool which_player)
    {
        if(which_player)
        {
            p1_hand.Add(p1_prizes[0]);
            p1_prizes.RemoveAt(0);
            if (p1_prizes.Count == 0)
                Win(which_player);
        }
        else
        {
            p2_hand.Add(p2_prizes[0]);
            p2_prizes.RemoveAt(0);
            if (p2_prizes.Count == 0)
                Win(which_player);
        }
    }

    void Promote(bool which_player, int index)
    {
        if(which_player)
        {
            p1_active_pkmn = p1_bench[index];
            p1_bench.RemoveAt(index);
        }
        else
        {
            p2_active_pkmn = p2_bench[index];
            p2_bench.RemoveAt(index);
        }
    }

    void Win(bool which_player)
    {

    }

    bool PlayTrainer(Trainer trainer, int index=0)
    {
        //effects:
        /*
         * drX - draw X cards
         * discard - discard own hand
         * discardX - discard X from own hand
         * gust - gust enemy mon out
         * switch - switch own mon out
         * pp - increase attack power this turn by 10
         */
        if (turn)
        {
            foreach (string effect in trainer._effects)
            {
                if (effect[0] == 'd' && effect[1] == 'r')
                {
                    for(int i = 0;i<(int)System.Char.GetNumericValue(effect[2]);i++)
                        p1_hand.Add(p1_deck.Draw());
                    return true;
                }
                else if(effect == "discard")
                {
                    while(p1_hand.Count > 0)
                    {
                        Discard(p1_hand[0], turn);
                        p1_hand.RemoveAt(0);
                    }
                    return true;
                }
                else if(effect == "gust")
                {
                    Pokemon temp = p2_active_pkmn;
                    p2_active_pkmn = p2_bench[index];
                    p2_bench[index] = temp;
                }
                else if(effect == "switch")
                {
                    Pokemon temp = p1_active_pkmn;
                    p1_active_pkmn = p1_bench[index];
                    p1_bench[index] = temp;
                }
                else if(effect == "pp")
                {
                    pp += 10;
                }
            }
        }
        return false;
    }
}
