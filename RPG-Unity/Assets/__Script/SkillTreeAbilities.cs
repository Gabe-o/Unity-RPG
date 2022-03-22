using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeAbilities : MonoBehaviour
{
    [Header("Sword Slash")]
    public bool unlockedSwordSlash = false;
    public float swordSlashDamage = 50f;
    public float swordSlashDelay = 1;
    public GameObject swordSlashSprite;
    private bool _canSwordSlash = true;

    [Header("Fire Stomp")] 
    public bool unlockedFireStomp = false;
    public float fireStompDamage = 100f;
    public float fireStompDelay = 1f;
    public GameObject fireStompSprite;
    private bool _canFireStomp = true;

    private void Update()
    {
        // Sword Slash on Q
        if (Input.GetKey(KeyCode.Q) && _canSwordSlash && unlockedSwordSlash)
        {
            _canSwordSlash = false;
            StartCoroutine(UseSwordSlash());
        }
        
        // Fire Stomp on E
        if (Input.GetKey(KeyCode.E) && _canFireStomp && unlockedFireStomp)
        {
            _canFireStomp = false;
            StartCoroutine(UseFireStomp());
        }
    }

    private IEnumerator UseSwordSlash()
    {
        // Display animation
        GameObject swordSlash = Instantiate(swordSlashSprite);
        swordSlash.transform.position = transform.position;
        swordSlash.GetComponent<ProjectileAttack>().SetSpin(1f);
        swordSlash.GetComponent<ProjectileAttack>().IsFollowPlayer();
        
        // Set Damage
        swordSlash.GetComponent<ProjectileAttack>().SetDamage(swordSlashDamage);
        // Destroy after 0.5 sec
        Destroy(swordSlash, 0.5f);

        // Wait delay before allowing another attack 
        yield return new WaitForSecondsRealtime(swordSlashDelay);

        _canSwordSlash = true;
    }

    private IEnumerator UseFireStomp()
    {
        // Display animation
        GameObject fireStomp = Instantiate(fireStompSprite);
        fireStomp.transform.position = transform.position;
        fireStomp.GetComponent<ProjectileAttack>().SetGrowth(1.002f);

        // Set Damage
        fireStomp.GetComponent<ProjectileAttack>().SetDamage(fireStompDamage);
        
        // Destroy after 0.5 sec
        Destroy(fireStomp, 1f);

        // Wait delay before allowing another attack 
        yield return new WaitForSecondsRealtime(fireStompDelay);

        _canFireStomp = true;
    }
}
