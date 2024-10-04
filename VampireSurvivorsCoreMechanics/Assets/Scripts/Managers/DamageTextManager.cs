using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField] DamageText damageTextPrefab;
    [Header("Pool")]
    private ObjectPool<DamageText> damageTextPool;
    private void Awake()
    {
        Enemy.onDamageTaken += DisplayDamageText;   
    }
    private void OnDestroy()
    {
        Enemy.onDamageTaken -= DisplayDamageText;
    }
    // Start is called before the first frame update
    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionGet,ActionOnRelease);
    }

    DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }
    void ActionGet(DamageText dmgText)
    {
        dmgText.gameObject.SetActive(true);
    }
    void ActionOnRelease(DamageText dmgText)
    {
        dmgText.gameObject.SetActive(false);    
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DisplayDamageText(int dmg, Vector2 enemyPosition)
    {
        DamageText dmgTextIndex = damageTextPool.Get();
        Vector3 spawnPosition = enemyPosition;
        dmgTextIndex.transform.position = spawnPosition;

        dmgTextIndex.Animate(dmg);
        LeanTween.delayedCall(1, () => damageTextPool.Release(dmgTextIndex));

    }
}
