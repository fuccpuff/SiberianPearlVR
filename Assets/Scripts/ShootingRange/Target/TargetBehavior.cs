using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public int points = 10;
   // private Animator animator;

    void Awake()
    {
  //      animator = GetComponent<Animator>();
    }

    public void RegisterHit()
    {
        ScoreManager.Instance.RegisterHit(points);
        Destroy(gameObject);
        //if (animator != null)
        //{
        //    animator.SetTrigger("close");
        //}
    }
}
