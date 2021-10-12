using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pulpit : MonoBehaviour
{
    public float lifeSpan = 5f;
    public TextMeshPro textMeshPro;
    public Animator animator;


    bool triggered;
    private void OnEnable()
    {
        animator.SetTrigger("ScaleOut");
        StartCoroutine(ActivateTimer());
    }
    private void OnDisable()
    {
        StopCoroutine(ActivateTimer());
    }

    public IEnumerator ActivateTimer()
    {
        yield return new WaitForSeconds(0.5f);
        lifeSpan = Random.Range(4f, 5f);
        triggered = false;
        while (lifeSpan > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            lifeSpan -= Time.deltaTime;
            textMeshPro.text = lifeSpan.ToString("0.00");
            if (lifeSpan < 2 && !triggered)
            {
                GameManager.instance.SpawnPulpitRandom();
                triggered = true;
            }
        }
        animator.SetTrigger("ScaleIn");
        yield return new WaitForSeconds(0.4f);
        this.gameObject.SetActive(false);
    }

}
