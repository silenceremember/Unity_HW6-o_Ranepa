using UnityEngine;

public class AnimationRandomizer : MonoBehaviour
{
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    [SerializeField]
    public void AnimatorChanger()
    {
        int randomValue = Random.Range(0, 2);
        _animator.SetInteger("AnimatorChanger", randomValue);
    }
}