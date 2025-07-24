using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private int index;
    
    public void TakeHit()
    {
        Debug.Log("Dummy hit");
        switch (index)
        {
           case 0:
               animator.SetTrigger("Take Hit Right");
               index = 1;
               break;
           case 1:
               animator.SetTrigger("Take Hit Left");
               index = 0;
               break;
        }
    }
}
