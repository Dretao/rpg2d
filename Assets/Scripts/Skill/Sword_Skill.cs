using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill Info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private float swordGravity;

    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        if (newSwordScript == null)
        {
            Debug.LogError("Sword_Skill_Controller component not found on the instantiated swordPrefab!");
            return;
        }

        newSwordScript.SetupSword(swordGravity, player.facingDir);
    }
}
