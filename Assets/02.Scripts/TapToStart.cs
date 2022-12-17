using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TapToStart : MonoBehaviour
{
    [SerializeField]
    private float fadeTime; // 페이드 되는 시간
    private TMP_Text textFade;  // 페이드 효과에 사용되는 텍스트

    private void Awake()
    {
        textFade = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        StartCoroutine(FadeInOut());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(FadeText(1, 0.2f));

            yield return StartCoroutine(FadeText(0.2f, 1));
        }
    }


    private IEnumerator FadeText(float start, float end)
    {
        float current = 0;
        float percent = 0;


        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = textFade.color;
            color.a = Mathf.Lerp(start, end, percent);
            textFade.color = color;

            yield return null;
        }
    }

}
