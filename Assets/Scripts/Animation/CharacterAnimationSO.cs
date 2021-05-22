using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Animation", menuName = "Character Animation")]
public class CharacterAnimationSO : ScriptableObject
{
    /** First index is top, then going clockwise. */
    public AnimationClip[] animations;

    public AnimationType animationType;

    public string AnimationAtAngle(float angle)
    {
        return animations[AngleIndex(angle)].name;
    }

    private int AngleIndex(float angle)
    {
        return (int)Mathf.Round((angle * animations.Length - 180) / 360);
    }
}
