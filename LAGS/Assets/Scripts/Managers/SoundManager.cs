using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [Header ("Audio Source")]
    [SerializeField] AudioSource MusicBackground;
    [SerializeField] AudioSource SFX;

    [Header("Audio Clip")]
    
    [Header("SoundTrack")]
    [SerializeField] AudioClip Nivel1;
    [SerializeField] AudioClip Nivel1Loop;

    [Header("Effects")]
    [Header("Arma")]
    [SerializeField] AudioClip shootPistola;
    [SerializeField] AudioClip reloadPistola;
    [SerializeField] AudioClip shootMetralleta;
    [SerializeField] AudioClip reloadMetralleta;
    [SerializeField] AudioClip shootEscopeta;
    [SerializeField] AudioClip reloadEscopeta;
    [Header ("Caminata")]
    [SerializeField] AudioClip caminataEnemie1;
    [SerializeField] AudioClip caminataEnemie2;
    [SerializeField] AudioClip caminataEnemie3;
    [SerializeField] AudioClip caminataJefe;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(PlaySongs());
    }

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

    IEnumerator PlaySongs()
    {
        MusicBackground.loop = false;
        MusicBackground.clip = Nivel1;
        MusicBackground.Play();

            yield return new WaitForSeconds(Nivel1.length -1.8f);

        MusicBackground.clip = Nivel1Loop;
        MusicBackground.loop = true;
        MusicBackground.Play();
    }
}
