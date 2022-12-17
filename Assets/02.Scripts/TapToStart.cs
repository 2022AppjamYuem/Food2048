using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TapToStart : MonoBehaviour
{
    [SerializeField]
    private float fadeTime; // ���̵� �Ǵ� �ð�
    private TMP_Text textFade;  // ���̵� ȿ���� ���Ǵ� �ؽ�Ʈ

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
