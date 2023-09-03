using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : ToSingletonMonoBehavior<GameManager>
{

    public GameObject PlayerObject;
    public MainGameEventPack GameEventPack = new MainGameEventPack();
    [SerializeField]
    AudioSource audioSource_;
    [SerializeField]
    AudioClip AudioClip;
    public int Score;
    [SerializeField]
    TextMeshProUGUI textMeshProUGUI_;
    [SerializeField]
    public Image Health;
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
        textMeshProUGUI_.text = Score.ToString();
        if (Score % 10 == 0)
        {
            audioSource_.PlayOneShot(AudioClip);
        }
    }
}
