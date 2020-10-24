using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class GetWordsAndPhonics : MonoBehaviour
{
    public static GetWordsAndPhonics curret;
    public Dictionary<string, string> store = new Dictionary<string, string>();
    // Load All Phonics from dictionary
    void Start()
    {
        string path = "Assets/Resources/Words_Phonics/entries.txt";

        StreamReader reader = new StreamReader(path, Encoding.UTF8);

        string[] lines = reader.ReadToEnd().Split('\n');
        foreach (var item in lines)
        {
            string[] split = item.Split(' ', '\t');
            if (!store.ContainsKey(split[0]))
            {
                store[split[0]] = split[2];
            }
        }
        curret = this;
    }


    public static string GetPhonics(string word)
    {
        word = word.Replace(".","");
        if (curret.store.ContainsKey(word))
        {
            return trimSpans(curret.store[word]);
        }

        curret.StartCoroutine(curret.Upd(word, (wrd, phonetic) => {
            curret.store[word] = phonetic.Trim();
        }));

        return "//";



    }
    IEnumerator Upd(string word, Action<string, string> callback)
    {
        using (WWW www = new WWW(@"http://92.237.222.105/view/GetPhonicsFromWord.php?word=" + word))
        {
            yield return www;
            print(www.text);
            callback(word, www.text);
        }
    }
    static string trimSpans(string input) {
        var m = Regex.Match(input, "<.*>");
        while (m.Success) { 
            input = input.Replace(m.Value, "");
            m = Regex.Match(input, "<.*>");
        }
        return input;
    }
}


/* (images index) = > Alphabetic Code(s)
 * (0) = > default
 * (1) = > /æ/ /ə/ /ʌ/
 * (2) = > /ɑ/ 
 * (3) = > /ɔ/ 
 * (4) = > /e/ /ɛ/ /ʊ/
 * (5) = > /ɚ/
 * (6) = > /j/ /i/ /ɪ/
 * (7) = > /w/ /u/
 * (8) = > /o/
 * (9) = > /ɑʊ/
 * (10)= > /ɔɪ/
 * (11)= > /ɑɪ/
 * (12)= > /h/
 * (13)= > /r/
 * (14)= > /l/
 * (15)= > /s/ /z/
 * (16)= > /ʃ/ /ʧ/ /ʤ/ /ʒ/
 * (17)= > /ɵ/ /ð/
 * (18)= > /f/ /v/
 * (19)= > /d/ /t/ /n/
 * (20)= > /k/ /g/ /ŋ/
 * (21)= > /p/ /b/ /m/ 
 * 
 */