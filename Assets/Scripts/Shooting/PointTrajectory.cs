using UnityEngine;

public class PointTrajectory : MonoBehaviour
{
    [SerializeField] private int _numberOfPoints;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Point _pointPrefab;

    private Vector2 _direction;
    private GameObject[] _points;
    
    private void Start()
    {
        _points = new GameObject[_numberOfPoints];

        for(int i =0; i < _numberOfPoints; i++)
        {
            _points[i] = Instantiate(_pointPrefab, transform.position, Quaternion.identity).gameObject;
        }

        CloseTrajectory();
    }

    public void CloseTrajectory()
    {
        for (int i = 0; i < _numberOfPoints; i++)
        {
            _points[i].gameObject.SetActive(false);
        }
    }

    public void ShowTrajectory()
    {
        for (int i = 0; i < _numberOfPoints; i++)
        {
            _points[i].gameObject.SetActive(true);
        }
    }

    public void Create(Vector2 position, float force)
    {
        Vector2 MousePos = -_mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 bowPos = position;
        _direction = MousePos - bowPos;

        for(int i = 0; i < _numberOfPoints; i++)
        {
            _points[i].transform.position = PointPosition(force, i * 0.1f, position);
        }
    }

    private Vector3 PointPosition(float force,float t, Vector2 position)
    {
        Vector2 currentointPos = position + (_direction.normalized * force * t) + 0.5f * Physics2D.gravity * (t * t);
        return currentointPos;
    }
}
