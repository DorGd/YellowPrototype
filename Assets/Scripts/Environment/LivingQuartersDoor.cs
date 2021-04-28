using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingQuartersDoor : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<SecurityBadge>() != null)
        {
            GetComponent<Animator>().SetBool("Open", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GetComponent<Animator>().SetBool("Open", false);
    }
}
