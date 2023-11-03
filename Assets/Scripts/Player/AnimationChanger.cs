using Spine;
using Spine.Unity;
using UnityEngine;

public class AnimationChanger : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private BoneRotator _boneLookRotate;

    private string _idleAnimation = "idle";
    private string _attackTargetAnimation = "attack_target";
    private string _attackAnimation = "attack_finish";
    private string _startAttackAnimation = "attack_start";

    private void Start()
    {
        _boneLookRotate.enabled = false;
        SetAnimation(_idleAnimation);
    }

    public void ChangeToIdleAnimation()
    {
        SetAnimation(_idleAnimation);
    }

    public void ChangeToAttackTargetAnimation()
    {
        _boneLookRotate.enabled=true;
        _skeletonAnimation.AnimationState.SetAnimation(0, _startAttackAnimation, false);
        _skeletonAnimation.AnimationState.AddAnimation(0, _attackTargetAnimation, true, 0f);
    }

    public void ChangeToAttackAnimation()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, _attackAnimation, false);
        _skeletonAnimation.AnimationState.AddAnimation(0, _idleAnimation, true, 0f);
        _boneLookRotate.enabled = false;
    }

    private void SetAnimation(string animationName)
    {
        if (_skeletonAnimation.SkeletonDataAsset.GetSkeletonData(true).FindAnimation(animationName) != null)
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, animationName, true);
        }
    }
}
