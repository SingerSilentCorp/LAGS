using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header ("Audio Source")]
    [SerializeField] AudioSource MusicBackground;
    [SerializeField] AudioSource SFX;

    [Header("Audio Clip")]
    [SerializeField] AudioClip shootPistola;
    [SerializeField] AudioClip reloadPistola;
    [SerializeField] AudioClip shootMetralleta;
    [SerializeField] AudioClip reloadMetralleta;
    [SerializeField] AudioClip shootEscopeta;
    [SerializeField] AudioClip reloadEscopeta;
    [SerializeField] AudioClip caminataEnemie1;
    [SerializeField] AudioClip caminataEnemie2;
    [SerializeField] AudioClip caminataEnemie3;
    [SerializeField] AudioClip caminataJefe;





    public void ShootPistola() => SFX.PlayOneShot(shootPistola);
    public void ReloadPistola() => SFX.PlayOneShot(reloadPistola);
    public void ShootMetralleta() => SFX.PlayOneShot(shootMetralleta);
    public void ReloadMetralleta() => SFX.PlayOneShot(reloadMetralleta);
    public void ShootEscopeta() => SFX.PlayOneShot(shootEscopeta);
    public void ReloadEscopeta() => SFX.PlayOneShot(reloadEscopeta);
    public void CaminataEnemie1() => SFX.PlayOneShot(caminataEnemie1);
    public void CaminataEnemie2() => SFX.PlayOneShot(caminataEnemie2);
    public void CaminataEnemie3() => SFX.PlayOneShot(caminataEnemie3);
    public void CaminataJefe() => SFX.PlayOneShot(caminataJefe);

}
