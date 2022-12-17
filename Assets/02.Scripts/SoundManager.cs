using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public AudioClip[] bgmList;

    private static SoundManager instance = null;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

        for (int i = 0; i < bgmList.Length; i++)
        {
            if (arg0.name == bgmList[i].name && bgmList[i] != null)
            {
                BgmPlay(bgmList[i]);
            }
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip, float volume = 1f)
    {
        GameObject sfxGo = new GameObject(sfxName + "Sound");
        AudioSource audioSource = sfxGo.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();
        Destroy(sfxGo, clip.length);
    }

    public void BgmPlay(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = 0.1f;
        bgmAudioSource.Play();
    }
    public void OnSliderEvent(float value)
    {
        bgmAudioSource.volume = value;
    }
}
