using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreatureAttack : MonoBehaviour
{
    public bool isCall;
    public void Herbivore(int amount)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,5);
        if(colliders.Length!=0)
        {
            foreach (Collider item in colliders)
            {
                if(item.GetComponent<TargetType>())
                {
                    if(item.GetComponent<TargetType>().m_Type==TargetType.Type.Carnivore)
                        item.GetComponent<Creature>().DealDamage(amount);
                }
            }
        }
    }
    public void Carnivore(int amount)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,5);
        if(colliders.Length!=0)
        {
            foreach (Collider item in colliders)
            {
                if(item.GetComponent<TargetType>())
                {
                    if(item.GetComponent<TargetType>().m_Type==TargetType.Type.Carnivore && item.name!=gameObject.name || item.GetComponent<TargetType>().m_Type==TargetType.Type.Herbivore)
                        item.GetComponent<Creature>().DealDamage(amount);
                }
            }
        }
    }
    public void Call()
    {
        isCall=true;
        StartCoroutine(ReturnFalse());
        // Collider[] colliders = Physics.OverlapSphere(transform.position,20);
        // if(colliders.Length!=0)
        // {
            // foreach (var item in colliders)
            // {
                // if(item.GetComponent<TargetType>().m_Type!=null)
                // {
                    // isCall=item.GetComponent<TargetType>().m_Type==TargetType.Type.Carnivore && item.name!=gameObject.name && item.name.Contains("raptor")?true:false;
                // }
            // }
        // }
    }
    IEnumerator ReturnFalse()
    {
        yield return new WaitForSeconds(5);
        isCall=false;
    }
}
