using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioClip[] _aud;
    AudioSource _audioSource;
        
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }




    public void ShootPistola() => _audioSource.PlayOneShot(_aud[0]);
    public void ReloadPistola() => _audioSource.PlayOneShot(_aud[0]);
    public void ShootMetralleta() => _audioSource.PlayOneShot(_aud[0]);
    public void ReloadMetralleta() => _audioSource.PlayOneShot(_aud[0]);
    public void ShootEscopeta() => _audioSource.PlayOneShot(_aud[0]);
    public void ReloadEscopeta() => _audioSource.PlayOneShot(_aud[0]);
    public void CaminataEnemie1() => _audioSource.PlayOneShot(_aud[0]);
    public void CaminataEnemie2() => _audioSource.PlayOneShot(_aud[0]);
    public void CaminataEnemie3() => _audioSource.PlayOneShot(_aud[0]);
    public void CaminataJefe() => _audioSource.PlayOneShot(_aud[0]);

}
