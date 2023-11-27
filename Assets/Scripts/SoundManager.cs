using UnityEngine;
public class SoundManager : Singleton<SoundManager>
{
    private AudioSource soundEffect;
    private AudioSource equipmentSoundEffect;
    private AudioSource BGM;
    private void Start()
    {       
        soundEffect = gameObject.AddComponent<AudioSource>();
        equipmentSoundEffect = gameObject.AddComponent<AudioSource>();
    }
    public void PlayBGM(AudioClip BGM, float volume)
    {
        if (BGM == null) return;
        this.BGM = transform.GetChild(0).GetComponent<AudioSource>();
        this.BGM.clip = BGM;
        this.BGM.volume = volume;
        this.BGM.loop = true;
        this.BGM.Play();
    }
    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        soundEffect.clip = clip;
        soundEffect.volume = 0.5f;
        soundEffect.loop = false;
        soundEffect.Play();
    }
    public void PlaySound(AudioClip clip, bool isEquipmentSound)
    {
        if (clip == null) return;
        if (isEquipmentSound)
        {
            equipmentSoundEffect.clip = clip;
            equipmentSoundEffect.volume = 0.5f;
            equipmentSoundEffect.loop = false;
            equipmentSoundEffect.Play();
            return;
        }
        soundEffect.clip = clip;
        soundEffect.volume = 0.5f;
        soundEffect.loop = false;
        soundEffect.Play();
    }
    public void PlaySound(AudioClip clip, float volume)
    {
        if (clip == null) return;
        soundEffect.clip = clip;
        soundEffect.volume = volume;
        soundEffect.loop = false;
        soundEffect.Play();
    }
    public void StopSound()
    {
        soundEffect.Stop();
    }
}