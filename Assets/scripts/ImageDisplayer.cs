using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplayer : MonoBehaviour
{
    public static ImageDisplayer id;
    public Sprite[] data;
    public Image img;
    public  List<int> linedup = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        id = this;
        StartCoroutine(Loop());
    }

    public static void AddToQueue(List<int> codes) {
        id.linedup.AddRange(codes);
    }



    // Update is called once per frame
    IEnumerator Loop()
    {
        while (true) {
            yield return new WaitForSeconds(0.16f);
            if (linedup.Count > 0) {
                img.sprite = data[linedup[0]];
                linedup.RemoveAt(0);
            }
        }
    }
}
