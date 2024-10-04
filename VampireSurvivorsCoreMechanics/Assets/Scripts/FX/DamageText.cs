    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [Header("Anim")]
    [SerializeField] Animator animator;
    [Header("UI")]
    [SerializeField] TextMeshPro dmgText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [NaughtyAttributes.Button]
    public void Animate(int dmg)
    {
        dmgText.text = dmg.ToString();
        animator.Play("DamageTextAnim");
    }
}
