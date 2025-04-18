using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header ("Audio Source")]
    [SerializeField] AudioSource MusicBackground;
    [SerializeField] AudioSource SFX;

    [Header("Audio Clip")]
    
    [Header("SoundTrack")]
    [SerializeField] AudioClip Nivel1;
    [SerializeField] AudioClip Nivel1Loop;
    [SerializeField] AudioClip Menu;

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
    [Header("Abrir Puerta")]
    [SerializeField] AudioClip abrirPuerta;

    private void Awake()
    {
        
            Instance = this;


        

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MusicBackground.clip = Menu;
            MusicBackground.Play();
        }
        else 
        {
            double startTime = AudioSettings.dspTime + 1.0; // Comienza dentro de 1 segundo

            // Programar el primer audio
            MusicBackground.clip = Nivel1;
            MusicBackground.PlayScheduled(startTime);

            // Calcular cuándo terminará el primer clip
            double clip1EndTime = startTime + Nivel1.length;

            // Programar el segundo audio justo cuando termine el primero
            SFX.clip = Nivel1Loop;
            SFX.PlayScheduled(clip1EndTime);
        }

    }

    private void Start()
    {
 
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
    public void AbrirPuerta() => SFX.PlayOneShot(abrirPuerta);

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
