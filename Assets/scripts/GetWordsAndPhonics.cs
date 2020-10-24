using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class GetWordsAndPhonics : MonoBehaviour
{
    static Dictionary<string, string> store = new Dictionary<string, string>();
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

    }


    public static string GetPhonics(string word)
    {
        word = word.Replace(".","");
        if (store.ContainsKey(word))
        {
            return trimSpans(store[word]);
        }

        //get word online
        // Create a request for the URL.
        System.Net.WebRequest request = System.Net.WebRequest.Create("https://dictionary.cambridge.org/dictionary/english/" + word);
        // If required by the server, set the credentials.
        request.Credentials = System.Net.CredentialCache.DefaultCredentials;
        // Get the response.
        System.Net.WebResponse response = request.GetResponse();
        // Display the status.

        // Get the stream containing content returned by the server.
        // The using block ensures the stream is automatically closed.
        using (StreamReader dataStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        {
            // Read the content.
            string responseFromServer = dataStream.ReadToEnd();
            string start = "<span class=\"pron dpron\">";
            string replacement = "<span class=\"ipa dipa lpr-2 lpl-1\">";
            string end = "</span>";
            string repl = "<span class=\"sp dsp\">";
            string s = responseFromServer.Substring(responseFromServer.IndexOf(start) + start.Length - 1);
            s = s.Substring(s.IndexOf(">/<") + 1);
            s = s.Substring(0, s.IndexOf(">/<") + 2);
            s = s.Replace(replacement, "");
            s = s.Replace(end, "");
            s = s.Replace(repl, "");


            store[word] = trimSpans(s);
            if (s.Length > 30) return "//";


            return trimSpans(s);
            // Display the content.
            //Console.WriteLine(responseFromServer);
        }

        // Close the response.
        response.Close();


        return "//";



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