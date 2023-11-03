using Spine;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SpineBone(dataField: "skeletonAnimation")]
    [SerializeField] private string _boneName;
    [SerializeField] private AnimationChanger _animationSwitcher;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Arrow _arrowPrefab; 
    [SerializeField] private PointTrajectory _pointPath; 
    [SerializeField] private float _maxPower = 10f;    

    private Vector2 _initialPosition;
    private bool _isBowActive = false;  
    private float _currentPower = 0f;    
    private Bone _bone;

    private void Start()
    {
        _bone = _skeletonAnimation.Skeleton.FindBone(_boneName);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCreating();
        }
        else if (_isBowActive && Input.GetMouseButton(0))
        {
            ContinueCreating();
        }
        else if (_isBowActive && Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    private void StartCreating()
    {
        _animationSwitcher.ChangeToAttackTargetAnimation();
        _initialPosition = _bone.GetSkeletonSpacePosition();
        _isBowActive = true;
        _pointPath.ShowTrajectory();
    }

    private void ContinueCreating()
    {
        _initialPosition = _bone.GetSkeletonSpacePosition();
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _currentPower = Mathf.Min((mousePosition - _initialPosition).magnitude, _maxPower);
        _pointPath.Create(_initialPosition, _currentPower);
    }

    private void Shoot()
    {
        _isBowActive = false;
        _pointPath.CloseTrajectory();

        Rigidbody2D arrow = Instantiate(_arrowPrefab, _initialPosition, Quaternion.identity).GetComponent<Rigidbody2D>();

        Vector2 shootDirection = -(_initialPosition + (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition)).normalized;
        arrow.velocity = shootDirection * _currentPower;

        _animationSwitcher.ChangeToAttackAnimation();

        _currentPower = 0f;
    }
}
