using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Notice_Board : Singleton<Notice_Board>
{
    GameObject Board;
    Text TMP_text;
    Coroutine coroutine;
    void Start()
    {
        Board = GameManager.Instance.Notice_Board;
        TMP_text = GetComponentInChildren<Text>();
        
    }

    public void ShowNotice(string message, float delay)
    {
        if (Board.gameObject.activeSelf == true) return;

        Board.gameObject.SetActive(true);
        if(!TMP_text) TMP_text = GetComponentInChildren<Text>();
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
