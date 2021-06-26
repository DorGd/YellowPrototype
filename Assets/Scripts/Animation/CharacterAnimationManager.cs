using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    Idle,
    Walk
}

public class CharacterAnimationManager : MonoBehaviour
{
    public CharacterAnimationSO[] animations;
    private Dictionary<AnimationType, CharacterAnimationSO> _animationsDict = new Dictionary<AnimationType, CharacterAnimationSO>();

    private Animator _animator;
    private AnimationType _currAnimationType = AnimationType.Idle;
    private string _currAnimation;

    private Transform _parentTransform;
    private Quaternion _originRotation = Quaternion.Euler(0, 45, 0);

    void Awake()
    {
        foreach (CharacterAnimationSO anim in animations)
        {
            _animationsDict.Add(anim.animationType, anim);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _parentTransform = transform.parent;
    }

    void Update()
    {
        transform.rotation = _originRotation;
        PlayAnimation(_currAnimationType);
    }

    public void PlayAnimation(AnimationType animation)
    {
        string newAnimation = _animationsDict[animation].AnimationAtAngle(_parentTransform.rotation.eulerAngles.y);
        if (_currAnimation == null || _currAnimation != newAnimation)
        {
            _currAnimationType = animation;
            _currAnimation = newAnimation;
            _animator.Play(_currAnimation);
        }
    }
}
