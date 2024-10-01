using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField] DamageText damageTextPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [NaughtyAttributes.Button]
    public void InstantiateDamageText()
    {
        Vector3 spawnPosition = Random.insideUnitCircle * Random.Range(1f, 4f);
        DamageText dmgTextIndex = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity ,transform);
        dmgTextIndex.Animate();
    }
}
