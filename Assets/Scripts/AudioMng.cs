using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMng : MonoBehaviour
{
    public AudioSource audioMusica;
    public AudioSource audioSFX;
    public AudioSource audioMarcha;

    // Controla se a marcha está pausada pelo Pause.
    private bool marchaPausada = false;

    // Controla se a marcha foi parada por Game Over
    // ou Level Complete.
    private bool marchaParada = false;

    // Guarda o estado anterior do Time.timeScale.
    private float ultimoTimeScale = 1f;


    // =========================================================
    // INICIALIZAÇÃO
    // =========================================================

    private void Awake()
    {
        if (audioMarcha != null)
        {
            audioMarcha.playOnAwake = false;
            audioMarcha.loop = true;
        }
    }

    private void Start()
    {
        Configuracao config = DBMng.ObterConfiguracao();

        AtualizarVolumesSemSalvar(
            config.volumeMusica,
            config.volumeSFX
        );

        // Verifica a cena atual.
        VerificarCenaAtual();

        ultimoTimeScale = Time.timeScale;
    }


    // =========================================================
    // CICLO
    // =========================================================

    private void Update()
    {
        if (audioMarcha == null)
            return;

        string nomeCena = SceneManager.GetActiveScene().name;

        // Só controla automaticamente a marcha
        // dentro das cenas de nível.
        if (!EhCenaDeNivel(nomeCena))
            return;

        // -----------------------------------------------------
        // JOGO ENTROU EM PAUSE
        // -----------------------------------------------------

        if (Time.timeScale == 0f && ultimoTimeScale > 0f)
        {
            // Só pausa se a marcha estiver tocando.
            if (audioMarcha.isPlaying)
            {
                PausarMarcha();
            }
        }

        // -----------------------------------------------------
        // JOGO VOLTOU DO PAUSE
        // -----------------------------------------------------

        if (Time.timeScale > 0f && ultimoTimeScale == 0f)
        {
            // Só continua se ela foi pausada pelo Pause.
            if (marchaPausada && !marchaParada)
            {
                ContinuarMarcha();
            }
        }

        ultimoTimeScale = Time.timeScale;
    }


    // =========================================================
    // TROCA DE CENA
    // =========================================================

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AoCarregarCena;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;

        PararMarcha();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;

        PararMarcha();
    }

    private void AoCarregarCena(
        Scene cena,
        LoadSceneMode modo
    )
    {
        VerificarCenaAtual();
    }


    // =========================================================
    // VERIFICAR CENA
    // =========================================================

    private void VerificarCenaAtual()
    {
        string nomeCena = SceneManager.GetActiveScene().name;

        // -----------------------------------------------------
        // MENU
        // -----------------------------------------------------

        if (nomeCena.Equals(
            "Menu",
            System.StringComparison.OrdinalIgnoreCase))
        {
            PararMarcha();
            return;
        }

        // -----------------------------------------------------
        // NÍVEIS
        // -----------------------------------------------------

        if (EhCenaDeNivel(nomeCena))
        {
            // Uma nova fase começa sempre tocando a marcha.
            marchaPausada = false;
            marchaParada = false;

            IniciarMarcha();

            return;
        }

        // -----------------------------------------------------
        // OUTRAS CENAS
        // -----------------------------------------------------

        PararMarcha();
    }


    private bool EhCenaDeNivel(string nomeCena)
    {
        return nomeCena.StartsWith(
            "Nivel",
            System.StringComparison.OrdinalIgnoreCase
        );
    }


    // =========================================================
    // VOLUMES
    // =========================================================

    public void AtualizarVolumes(
        float volumeMusica,
        float volumeSFX)
    {
        AtualizarVolumesSemSalvar(
            volumeMusica,
            volumeSFX
        );

        DBMng.SalvarVolumes(
            volumeMusica,
            volumeSFX
        );
    }

    private void AtualizarVolumesSemSalvar(
        float volumeMusica,
        float volumeSFX)
    {
        if (volumeMusica <= 0.01f)
            volumeMusica = 0f;

        if (volumeSFX <= 0.01f)
            volumeSFX = 0f;

        if (audioMusica != null)
        {
            audioMusica.volume = volumeMusica;
        }

        if (audioSFX != null)
        {
            audioSFX.volume = volumeSFX;
        }

        if (audioMarcha != null)
        {
            audioMarcha.volume = volumeSFX;
        }
    }


    // =========================================================
    // MÚSICA
    // =========================================================

    public void TocarMusica(AudioClip musica)
    {
        if (audioMusica == null || musica == null)
            return;

        audioMusica.Stop();

        audioMusica.clip = musica;
        audioMusica.loop = true;
        audioMusica.Play();
    }

    public void TocarMusicaUmaVez(AudioClip musica)
    {
        if (audioMusica == null || musica == null)
            return;

        audioMusica.Stop();

        audioMusica.clip = musica;
        audioMusica.loop = false;
        audioMusica.Play();
    }

    public void PararMusica()
    {
        if (audioMusica == null)
            return;

        audioMusica.Stop();
        audioMusica.clip = null;
    }


    // =========================================================
    // SFX
    // =========================================================

    public void TocarSom(AudioClip som)
    {
        if (audioSFX == null || som == null)
            return;

        audioSFX.PlayOneShot(som);
    }


    // =========================================================
    // MARCHA
    // =========================================================

    // ---------------------------------------------------------
    // INICIAR MARCHA
    // ---------------------------------------------------------

    public void IniciarMarcha()
    {
        if (audioMarcha == null)
            return;

        audioMarcha.loop = true;

        marchaPausada = false;
        marchaParada = false;

        audioMarcha.Stop();
        audioMarcha.Play();
    }


    // ---------------------------------------------------------
    // PAUSAR MARCHA
    // ---------------------------------------------------------

    public void PausarMarcha()
    {
        if (audioMarcha == null)
            return;

        if (audioMarcha.isPlaying)
        {
            audioMarcha.Pause();
        }

        marchaPausada = true;
    }


    // ---------------------------------------------------------
    // CONTINUAR MARCHA
    // ---------------------------------------------------------

    public void ContinuarMarcha()
    {
        if (audioMarcha == null)
            return;

        // Se foi parada por Game Over ou Level Complete,
        // não deve voltar simplesmente pelo Pause.
        if (marchaParada)
            return;

        audioMarcha.UnPause();

        marchaPausada = false;
    }


    // ---------------------------------------------------------
    // PARAR MARCHA
    // ---------------------------------------------------------

    public void PararMarcha()
    {
        if (audioMarcha == null)
            return;

        audioMarcha.Stop();

        marchaPausada = false;
        marchaParada = true;
    }


    // =========================================================
    // PARAR DEFINITIVAMENTE
    // =========================================================

    // Use especificamente para:
    // - Game Over
    // - Level Complete
    // - Menu
    public void PararMarchaDefinitivamente()
    {
        if (audioMarcha == null)
            return;

        audioMarcha.Stop();

        marchaPausada = false;
        marchaParada = true;
    }
}