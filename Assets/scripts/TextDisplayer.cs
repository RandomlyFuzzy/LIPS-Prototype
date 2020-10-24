using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    public static TextDisplayer td = new TextDisplayer();
    private static string displayText = "";
    private static int index = 0;

    public int ind = 0;
    public string txt = "";
    public Text m_textbox;
    public Text phonics;

    private string lastWord = "";


    // Start is called before the first frame update
    public void Start(){
        td = this;
        StartCoroutine(Displaytext());
    }

    void DisplayCurrentString() {
        ind = index;
        txt = displayText;
        UpdatePosition();
    }

    void UpdatePosition() {
        TextGenerator textGen = new TextGenerator();
        TextGenerationSettings generationSettings = m_textbox.GetGenerationSettings(m_textbox.rectTransform.rect.size);
        float widthfull = textGen.GetPreferredWidth(txt, generationSettings);
        float width = -textGen.GetPreferredWidth(txt.Substring(0, index% txt.Length), generationSettings);
        float half = textGen.GetPreferredWidth("" + txt[index % txt.Length], generationSettings) / 2;
        float height = textGen.GetPreferredHeight(txt, generationSettings);

        m_textbox.text = txt;
        m_textbox.rectTransform.sizeDelta = new Vector2(widthfull, height);
        m_textbox.transform.localPosition = new Vector3(((widthfull / 2)) + (width - half), m_textbox.transform.localPosition.y, m_textbox.transform.localPosition.z);
    }



    public static void SetTextToDisplay(string val) {

        if (val.LastIndexOf(' ') != -1) {
            val = val.Substring(0, val.LastIndexOf(' '));
        }

        td.m_textbox.text = displayText = val;
        td.DisplayCurrentString();
    }

    public IEnumerator Displaytext() {
        while (true) { 
            yield return new WaitForSeconds(.1f);
            try{
                index++;
                index = index % displayText.Length;
                string temp = displayText.Substring(index);
                string temp2 = displayText.Substring(0,index );

                int id = temp2.LastIndexOf(" ");
                if (id == -1)
                {
                    id = 0;
                }
                int id2 = temp.IndexOf(" ");
                if (id2 == -1)
                {
                    if (temp.IndexOf(".") != -1) { 
                        id2 = temp.IndexOf(".");
                    }
                    else { 
                        id2 = temp.Length-1;
                    }
                }
                string added = temp2.Substring(id) + temp.Substring(0,id2+1);
                string rel = (GetWordsAndPhonics.GetPhonics(added));
                if (added != lastWord && rel != ""){
                    phonics.text = rel.Trim();
                    ImageDisplayer.AddToQueue(PhonicDecoder.GetImages(GetWordsAndPhonics.GetPhonics(added)));
                }
                lastWord = added;
                DisplayCurrentString();
            }catch (Exception ex) { }
        }
    }
}
