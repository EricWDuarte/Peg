using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public AudioClip clang;
    AudioSource audioSource;

    public static Game_Manager manager;

    public bool audioOn = true;

    // Start is called before the first frame update
    void Start()
    {
        manager = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene ()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void PlaySound()
    {
        if(audioOn)
        audioSource.PlayOneShot(clang);
    }

    public void ChangeAudio()
    {
        audioOn = !audioOn;
    }
}
