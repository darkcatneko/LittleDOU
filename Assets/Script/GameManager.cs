using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : ToSingletonMonoBehavior<GameManager>
{
    
    public GameObject PlayerObject;
    public MainGameEventPack GameEventPack = new MainGameEventPack();
    [SerializeField]
    AudioSource audioSource_;
    [SerializeField]
    AudioClip AudioClip;
    public int Score;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlusScore()
    {
        Score++;
        if (Score %10 ==0)
        {
            audioSource_.PlayOneShot(AudioClip);
        }
    }
}
