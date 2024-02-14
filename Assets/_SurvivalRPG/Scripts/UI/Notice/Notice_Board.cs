using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Notice_Board : Singleton<Notice_Board>
{
    [SerializeField] GameObject Board;
    Text TMP_text;
    Coroutine coroutine;
    void Start()
    {
        TMP_text = GetComponentInChildren<Text>();
    }

    public void ShowNotice(string message = "", float delay = 2)
    {
        Board.gameObject.SetActive(true);

        TMP_text.text = message;
        
        if (coroutine != null) StopCoroutine(coroutine);
        StartCoroutine(_ShowNotice(delay));
    }

    IEnumerator _ShowNotice(float delay)
    {
 
        yield return new WaitForSeconds(delay);

        Board.gameObject.SetActive(false);
    }
}
