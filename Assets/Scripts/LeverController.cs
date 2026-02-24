using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class LeverController : MonoBehaviour
{
    public bool isOn = false;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOn", isOn);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            this.isOn = !this.isOn;
            animator.SetBool("isOn", isOn);
        }
    }
}