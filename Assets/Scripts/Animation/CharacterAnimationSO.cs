using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Animation", menuName = "Character Animation")]
public class CharacterAnimationSO : ScriptableObject
{
    public AnimationType animationType;

    /** First index is top/back, then going clockwise. */
    public AnimationClip[] animations;

    public string AnimationAtAngle(float angle)
    {
        return animations[AngleIndex(angle)].name;
    }

    private int AngleIndex(float angle)
    {
        return Mathf.FloorToInt(((angle * animations.Length - 180) / 360) % animations.Length);
    }
}
