using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource dayBgm;
    [SerializeField] private AudioSource nightBgm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            bgm.Play();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            bgm.Stop();
        }
    }

    public void PlayBgm()
    {
        bgm.Play();
    }

    public void StopBgm()
    {
        bgm.Stop();
    }

    public void PlayDayDgm()
    {
        dayBgm.Play();
    }

    public void StopDayDgm()
    {
        dayBgm.Stop();
    }

    public void PlayNightBgm()
    {
        nightBgm.Play();
    }

    public void StopNightBgm()
    {
        nightBgm.Stop();
    }

    public void SetBgmVolume(float volume)
    {
        bgm.volume = volume;
    }

    public void SetDayBgmVolume(float volume)
    {
        dayBgm.volume = volume;
    }

    public void SetNightVolume(float volume)
    {
        nightBgm.volume = volume;
    }
}
