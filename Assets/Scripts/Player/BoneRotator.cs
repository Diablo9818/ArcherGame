using Spine;
using Spine.Unity;
using UnityEngine;

public class BoneRotator : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SpineBone(dataField: "skeletonAnimation")]
    [SerializeField] private string _boneNameGun;
    [SerializeField] private Camera _mainCamera;
    
    private Bone _bone;

    private void Start()
    {
        _bone = _skeletonAnimation.Skeleton.FindBone(_boneNameGun);
    }

    private void Update()
    {
        RotateBoneTowardsMouse();
    }

    private void RotateBoneTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _mainCamera.transform.position.z;
        Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        Vector3 skeletonSpacePoint = _skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);

        float rotationAngle = Mathf.Atan2(skeletonSpacePoint.y, skeletonSpacePoint.x) * Mathf.Rad2Deg;

        _bone.Rotation = (rotationAngle + 180);

        _skeletonAnimation.Skeleton.UpdateWorldTransform();
    }
}
