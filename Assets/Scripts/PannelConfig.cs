using UnityEngine;
using UnityEngine.UI;

public class PannelConfig : MonoBehaviour
{
    public Slider sldMusica;
    public Slider sldSFX;

    private void OnEnable()
    {
        Configuracao config = DBMng.ObterConfiguracao();

        sldMusica.value = config.volumeMusica;
        sldSFX.value = config.volumeSFX;
    }

    public void AlterarVolumeSliderMusica()
    {
        DBMng.SalvarVolumes(sldMusica.value, sldSFX.value);
        GameManager.Audios.AtualizarVolumes(
            sldMusica.value,
            sldSFX.value
        );
    }

    public void AlterarVolumeSliderSFX()
    {
        DBMng.SalvarVolumes(sldMusica.value, sldSFX.value);
        GameManager.Audios.AtualizarVolumes(
            sldMusica.value,
            sldSFX.value
        );
    }
}