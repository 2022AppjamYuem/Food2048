using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class TapToStart : MonoBehaviour
{
    [SerializeField]
    private float fadeTime; // 페이드 되는 시간
    private TMP_Text textFade;  // 페이드 효과에 사용되는 텍스트

    [SerializeField]
    private RectTransform titlePanelRectTr;

    private bool chageMain;

    private void Awake()
    {
        textFade = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (GameManager.Instance.isStart == true)
        {
            titlePanelRectTr.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(FadeInOut());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && chageMain == false)
        {
            StartCoroutine(MoveTitlePanel());
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

    private IEnumerator MoveTitlePanel()
    {
        GameManager.Instance.isStart = true;

        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 0.5f;
            
            titlePanelRectTr.anchoredPosition = Vector2.Lerp(Vector2.zero, new Vector2(-1500, 0), percent);

            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        titlePanelRectTr.gameObject.SetActive(false);

        chageMain = true;
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
