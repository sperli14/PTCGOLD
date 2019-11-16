using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPreview : MonoBehaviour
{
    [SerializeField]
    Sprite psychic;
    [SerializeField]
    Sprite PromoMewtwo;
    [SerializeField]
    Sprite mad;
    [SerializeField]
    Sprite bill;
    public void ChangeSprite(string s)
    {
        if(s=="PR003")
        {
            GetComponent<SpriteRenderer>().sprite = PromoMewtwo;
        }
        if (s == "BS091")
        {
            GetComponent<SpriteRenderer>().sprite = bill;
        }
        if (s == "BS102")
        {
            GetComponent<SpriteRenderer>().sprite = psychic;
        }
        if(s == "BSXXX")
        {
            GetComponent<SpriteRenderer>().sprite = mad;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
