using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI compliment;
    [SerializeField] TextMeshProUGUI credits;

    string text1 = "You are SO amazing!";
    string text2 = "Nobody ever makes it this far!";
    string text3 = "That was incredible!";
    string text4 = "You're almost as good as my kid brother!";

    string credits1 = "Brackeys (YouTube) - Unity Educational Videos";
    string credits2 = "Kenney Game Assets - Art/Animation - www.kenney.nl";
    string credits3 = "Music by Matthew Pablo - Composer - www.matthewpablo.com";
    string credits4 = "FoxSynergy - Composer - 'Metallic Mistress' ";
    string credits5 = "Pheonton - Composer - 'One' ";
    string credits6 = "Otto Halmen - Composer - 'Death Is Just Another Path' ";
    string credits7 = "Vernon Adams - Font Designer - Amatic SC";
    string credits8 = "Kimberly Geswein - Font Designer - Gloria Halleluhah";


    float timer = 0f;
    float timer2 = 0f;

    private void Start()
    {
        compliment.text = text1;
        credits.text = credits1;
    }
        
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        // Yes, this is lazy code. The idea was a late implementation done while sleepy. :D
        if (timer > 3f && timer < 6f) { compliment.text = text2; }
        else if (timer > 6f && timer < 9f) { compliment.text = text3; }
        else if (timer > 9f && timer < 12f) { compliment.text = text4; }
        else if (timer > 12f) { timer = 0; compliment.text = text1; }

        if (timer2 > 5f && timer2 < 10f) { credits.text = credits2; }
        else if (timer2 > 10f && timer2 < 15f) { credits.text = credits3; }
        else if (timer2 > 15f && timer2 < 20f) { credits.text = credits4; }
        else if (timer2 > 20f && timer2 < 25f) { credits.text = credits5; }
        else if (timer2 > 25f && timer2 < 30f) { credits.text = credits6; }
        else if (timer2 > 30f && timer2 < 35f) { credits.text = credits7; }
        else if (timer2 > 35f && timer2 < 40f) { credits.text = credits8; }
        else if (timer2 > 40f) { timer2 = 0; credits.text = credits1; }
    }
}
