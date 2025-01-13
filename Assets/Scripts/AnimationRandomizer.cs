using UnityEngine;

public class AnimationRandomizer : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Добавляем атрибут для явного указания, что метод вызывается из Animation Event
    [SerializeField]
    public void AnimatorChanger() // Изменили имя метода, чтобы оно совпадало с тем, что в Animation Event
    {
        int randomValue = Random.Range(0, 2);
        animator.SetInteger("AnimatorChanger", randomValue);
    }
}