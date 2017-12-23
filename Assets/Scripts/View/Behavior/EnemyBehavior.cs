using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(healthComponent))]

public class EnemyBehavior : MonoBehaviour {

    private Animator animator;
    private MeshFader meshFader;
    private AudioSource audioSource;
    public AudioClip hurtClip;
    private healthComponent healthComponent;

    private void Awake(){
        animator = GetComponent<Animator>();
        meshFader = GetComponent<MeshFader>();
        audioSource = GetComponent<AudioSource>();
        healthComponent = GetComponent<healthComponent>();
    }

    private void OnEnable() {
        StartCoroutine(meshFader.FadeIn());
        healthComponent.Init(100);
    }

    private void DoDamage(int attack) {
        animator.SetTrigger("hurt");
        audioSource.clip = hurtClip;
        audioSource.Play();
        healthComponent.Hurt(attack);
    }

    private void Update() {
        if (healthComponent.IsOver)
        
            return;


        if (Input.GetButtonDown("Fire1")) {
            DoDamage(10);
        }
    }

}
