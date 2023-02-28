using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PhysicalWorld : MonoBehaviour
{
    public List<PhysicalBody> bodyList = new List<PhysicalBody>();

    private void Awake()
    {
        InvokeRepeating("CollisionDetection", 0, 0.2f); // check for collision every 0.2 seconds
    }

    private void CollisionDetection()
    {
        for (int bodyA = 0; bodyA < bodyList.Count; bodyA++)
        {
            for (int bodyB = bodyA + 1; bodyB < bodyList.Count; bodyB++)
            {
                if (bodyList[bodyA] != null && bodyList[bodyB] != null)
                {
                    float distance = Vector3.Distance(bodyList[bodyA].Position, bodyList[bodyB].Position);
                    float radiusSum = bodyList[bodyA].radius + bodyList[bodyB].radius;

                    if(distance <= radiusSum)
                    {
                        CollisionResolution(bodyList[bodyA], bodyList[bodyB]);
                    }
                }
            }
        }
    }

    private void CollisionResolution(PhysicalBody bodyA, PhysicalBody bodyB)
    {
        DamageHandler damageHandlerBodyA = bodyA.gameObject.GetComponent<DamageHandler>();
        DamageHandler damageHandlerBodyB = bodyB.gameObject.GetComponent<DamageHandler>();

        // collision between player ammo and enemy
        if(bodyA.gameObject.CompareTag("PlayerAmmo") && bodyB.gameObject.CompareTag("Enemy"))
        {
            damageHandlerBodyA.TakeDamage(); // ammo loses health
            damageHandlerBodyB.BounceBack(bodyA); // enemy bounce
            damageHandlerBodyB.TakeDamage(); // enemy loses health
        }
        else if(bodyA.gameObject.CompareTag("Enemy") && bodyB.gameObject.CompareTag("PlayerAmmo"))
        {
            damageHandlerBodyB.TakeDamage(); // ammo loses health
            damageHandlerBodyA.BounceBack(bodyB); // enemy bounce
            damageHandlerBodyA.TakeDamage(); // enemy loses health
        }
        // collision between player ammo and enemy ammo
        else if(bodyA.gameObject.CompareTag("PlayerAmmo") && bodyB.gameObject.CompareTag("EnemyAmmo"))
        {
            damageHandlerBodyA.TakeDamage(); // ammo loses health
            damageHandlerBodyB.TakeDamage(); // ammo loses health
        }
        else if(bodyA.gameObject.CompareTag("EnemyAmmo") && bodyB.gameObject.CompareTag("PlayerAmmo"))
        {
            damageHandlerBodyB.TakeDamage(); // ammo loses health
            damageHandlerBodyA.TakeDamage(); // ammo loses health
        }
        // collision between player and enemy ammo
        else if(bodyA.gameObject.CompareTag("Player") && bodyB.gameObject.CompareTag("EnemyAmmo"))
        {
            damageHandlerBodyA.TakeDamage(); // player loses health
            damageHandlerBodyB.TakeDamage(); // ammo loses health
        }
        else if(bodyA.gameObject.CompareTag("EnemyAmmo") && bodyB.gameObject.CompareTag("Player"))
        {
            damageHandlerBodyB.TakeDamage(); // player loses health
            damageHandlerBodyA.TakeDamage(); // ammo loses health
        }
        // collision between player and enemy
        else if(bodyA.gameObject.CompareTag("Player") && bodyB.gameObject.CompareTag("Enemy"))
        {
            damageHandlerBodyA.TakeDamage(); // player loses health
        }
        else if(bodyA.gameObject.CompareTag("Enemy") && bodyB.gameObject.CompareTag("Player"))
        {
            damageHandlerBodyB.TakeDamage(); // player loses health
        }
    }

    public void AddBody(PhysicalBody newBody)
    {
        bodyList.Add(newBody);
    }
}