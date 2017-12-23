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
    public AudioClip deadClip;
    public EnemyData enemyData;

    public bool IsDead {
        get {
            return healthComponent.IsOver;
        }
    }

    private void Awake(){
        animator = GetComponent<Animator>();
        meshFader = GetComponent<MeshFader>();
        audioSource = GetComponent<AudioSource>();
        healthComponent = GetComponent<healthComponent>();
    }

    private void OnEnable() {
        StartCoroutine(meshFader.FadeIn());
    }
    [ContextMenu("Test Execute")]
    private void TestExecute() {
        StartCoroutine(Execute(enemyData));
    }
    public IEnumerator Execute(EnemyData enemyData) {
        healthComponent.Init(enemyData.health);
        while (IsDead == false) {
            yield return null;
        }

        animator.SetTrigger("die");
        audioSource.clip = deadClip;
        audioSource.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        yield return StartCoroutine(meshFader.FadeOut());
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
