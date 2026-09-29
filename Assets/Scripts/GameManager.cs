using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static CanvasMensagemMng CanvasMensagem;
    public static AudioMng Audios;
    public static GameDataMng GameData;

    private void Awake()
    {
        if(Instance == null)
        {            
            CanvasMensagem = GetComponentInChildren<CanvasMensagemMng>();
            Audios = GetComponentInChildren<AudioMng>();
            GameData = GetComponentInChildren<GameDataMng>();
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
