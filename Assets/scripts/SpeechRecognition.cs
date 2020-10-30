using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


public class SpeechRecognition : MonoBehaviour
{
     [SerializeField]
    private Text m_Hypotheses;

    [SerializeField]
    private Text m_Recognitions;

    private DictationRecognizer m_DictationRecognizer;



    string last = "";
    void Start()
    {
        m_DictationRecognizer = new DictationRecognizer();
        m_DictationRecognizer.AutoSilenceTimeoutSeconds = 0.4f;

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            //Debug.LogFormat("Dictation result: {0}", text);
            //m_Recognitions.text = text + "\n";

            //TextDisplayer.SetTextToDisplay(text);
        };
        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            //Debug.LogFormat("Dictation hypothesis: {0}", text);
            //m_Hypotheses.text = text;
            if (text.Length == 0) return;

            TextDisplayer.SetTextToDisplay(text);
            last = text;
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause == DictationCompletionCause.TimeoutExceeded || completionCause == DictationCompletionCause.Complete)
            {
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);

                TextDisplayer.SetTextToDisplay(last+ ". ");
                m_DictationRecognizer.Stop();
                m_DictationRecognizer.Start();
            }
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

        m_DictationRecognizer.Start();
    }
}
