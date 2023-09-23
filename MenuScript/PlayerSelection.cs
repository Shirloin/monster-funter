using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public ParticleSystem particle1, particle2;
    public AudioSource audio1, audio2;
    public Light light1, light2;
    public Animator player1, player2;

    private static PlayerSelection ps;
    public static PlayerSelection Instance()
    {
        if (ps == null)
        {
            return ps = new PlayerSelection();
        }
        return ps;
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        Reset();
        particle1.Play();
        audio1.Play();
        light1.enabled = true;
        player1.SetTrigger("animation");
        player1.ResetTrigger("idle");
    }

    public void Reset()
    {
        particle1.Stop();
        particle2.Stop();
        audio1.Stop();
        audio2.Stop();
        light1.enabled = false;
        light2.enabled = false;
        player1.ResetTrigger("animation");
        player2.ResetTrigger("animation");
        player1.SetTrigger("idle");
        player2.SetTrigger("idle");
    }
}
