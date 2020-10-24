using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonicDecoder : MonoBehaviour{


    public static List<int> GetImages(string codes) {
        List<int> ret = new List<int>();
        for (int i = 1; i < codes.Length-1; i++)
        {
            switch (codes[i])
            {
                case 'ɔ':
                    if (codes[i + 1] == 'ɪ')
                    {
                        ret.Add(10);
                        i++;
                    }
                    else { 
                        ret.Add(3);
                    }
                    break;
                case 'ɑ':
                    if (codes[i + 1] == 'ʊ'){
                        ret.Add(9);
                        i++;
                    }else if (codes[i + 1] == 'ɪ') { 
                        ret.Add(11);
                        i++;
                    }else {
                        ret.Add(2);
                    }
                    break;
                case 'æ':
                case 'ə':
                case 'ʌ':
                    ret.Add(1);
                    break;
                case 'e':
                case 'ɛ':
                case 'ʊ':
                    ret.Add(3);
                    break;
                case 'ɚ':
                    ret.Add(5);
                    break;
                case 'j':
                case 'i':
                case 'ɪ':
                    ret.Add(6);
                    break;
                case 'w':
                case 'u':
                    ret.Add(7);
                    break;
                case 'o':
                    ret.Add(8);
                    break;
                case 'h':
                    ret.Add(12);
                    break;
                case 'r':
                    ret.Add(13);
                    break;
                case 'l':
                    ret.Add(14);
                    break;
                case 's':
                case 'z':
                    ret.Add(15);
                    break;
                case 'ʃ':
                case 'ʧ':
                case 'ʤ':
                case 'ʒ':
                    ret.Add(16);
                    break;
                case 'ɵ':
                case 'ð':
                    ret.Add(17);
                    break;
                case 'f':
                case 'v':
                    ret.Add(18);
                    break;
                case 'd':
                case 't':
                case 'n':
                    ret.Add(19);
                    break;
                case 'g':
                case 'k':
                case 'ŋ':
                    ret.Add(20);
                    break;
                case 'p':
                case 'b':
                case 'm':
                    ret.Add(21);
                    break;
                default:
                    break;
            }
        }
        //ret.Add(0);
        return ret;
    }
    public static string GetRelivantCodes(string input) {

        input = input.Replace("weak", "");
        string output = "";
        for (int i = 1; i < input.Length-1; i++)
        {
            switch (input[i])
            {
                case 'ɔ':
                    if (input[i + 1]== 'ɪ') {
                        output += input[i];
                        output += input[i + 1];
                        i++;
                    } else {
                        output += input[i];
                    }
                    break;
                case 'ɑ':
                    if (input[i + 1] == 'ʊ' || input[i + 1] == 'ɪ')
                    {
                        output += input[i];
                        output += input[i + 1];
                        i++;
                    } else { 
                        output += input[i];
                    }
                    break;
                case 'æ':
                case 'ə':
                case 'ʌ':
                case 'e':
                case 'ɛ':
                case 'ʊ':
                case 'ɚ':
                case 'j':
                case 'i':
                case 'ɪ':
                case 'w':
                case 'u':
                case 'o':
                case 'h':
                case 'r':
                case 'l':
                case 's':
                case 'z':
                case 'ʃ':
                case 'ʧ':
                case 'ʤ':
                case 'ʒ':
                case 'ɵ':
                case 'ð':
                case 'f':
                case 'v':
                case 'd':
                case 't':
                case 'n':
                case 'g':
                case 'ŋ':
                case 'p':
                case 'k':
                case 'b':
                case 'm':
                    output += input[i];
                    break;
                default:
                    break;
            }
        }
        return output;
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