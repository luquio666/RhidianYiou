using UnityEngine;
using System.Collections;

public class ModuleCarMoveBetweenPoints : Module {

	public int NextTarget;
	public float Speed = 3f;
	public float CollCheckDistance = 2.4f; // 2.4 is a tested value (26/6/22)
	public float VertCheckDist = 0.469f;
	public float HorCheckDist = 0.564f;
	public float HorCheckDistDiff = 0.253f;
	[Space]
	public Transform[] Points;

	private ModuleCar _car;
	private Vector3[] _points;
	private Vector3 _nextPos;
	private Vector3 _direction0;
	private Vector3 _direction1;
	private Vector3 _direction2;
	private float _currentSpeed;

	private float _maxTimeStopped = 3f;
	private float _timeStopped;
	private float _maxTimeSwitched = 1f;
	private float _timeSwitched;

	void Awake()
    {
		_car = this.GetComponent<ModuleCar>();
		_maxTimeStopped *= Random.Range(1f, 1.5f);
		_maxTimeSwitched *= Random.Range(1f, 1.5f);

		// Get all transforms and assign its positions
		_points = new Vector3[Points.Length];
        for (int i = 0; i < Points.Length; i++)
        {
			_points[i] = Points[i].position;
        }
		_nextPos = _points[NextTarget];
	}

	bool _enableBackwards;

	void Update()
	{
		_direction0 = (_points[NextTarget] - this.transform.position).normalized * CollCheckDistance;

		// Moving horizontal, splin them on Y axis
		if (Mathf.Abs(_direction0.x) > 0)
		{
			_direction0 = new Vector3(_direction0.x, _direction0.y - (HorCheckDistDiff / 2), 0);
			_direction1 = new Vector3(_direction0.x, HorCheckDist - HorCheckDistDiff, 0);
			_direction2 = new Vector3(_direction0.x, - HorCheckDist, 0);
		}
		// Moving vertical, split them on X axis
		else
		{
			_direction1 = new Vector3(VertCheckDist, _direction0.y, 0);
			_direction2 = new Vector3(-VertCheckDist, _direction0.y, 0);
		}

		bool canMove0 = CanMoveInDirection(_direction0);
		bool canMove1 = CanMoveInDirection(_direction1);
		bool canMove2 = CanMoveInDirection(_direction2);

		if (_timeStopped > _maxTimeStopped)
		{
			_enableBackwards = true;
			_car.EnableSetSprite = false;
			_timeStopped = 0;
			SetPrevTarget();
		}

		if (_enableBackwards)
		{
			_timeSwitched += Time.deltaTime;
			if (_timeSwitched > _maxTimeSwitched)
			{
				_enableBackwards = false;
				_car.EnableSetSprite = true;
				SetNextTarget();
				
				_timeSwitched = 0;
			}
		}


		bool canMove = (canMove0 && canMove1 && canMove2);
		if (canMove)
		{
			// Accelerate till max speed
			if (_currentSpeed < Speed)
			{
				_currentSpeed += Time.deltaTime * 2.5f;
				if (_currentSpeed > Speed)
					_currentSpeed = Speed;
			}
		}
		else
		{
			// Deccelerates till 0
			if (_currentSpeed > 0)
			{
				_currentSpeed -= Time.deltaTime * 20f;
				if (_currentSpeed < 0)
					_currentSpeed = 0;
			}
			_timeStopped += Time.deltaTime;
		}
		
		
		MoveObject();
	}

	private bool CanMoveInDirection(Vector3 direction)
	{
		bool result = Physics.Linecast(this.transform.position, this.transform.position + direction) == false;
		return result;
	}

	private void MoveObject () 
	{	
		this.transform.position = Vector3.MoveTowards(
			this.transform.position,
			_nextPos,
			_currentSpeed * Time.deltaTime
		);

		if(this.transform.position == _nextPos) 
		{
			SetNextTarget();
		}
		_nextPos = _points[NextTarget];
	}

	private void SetNextTarget()
	{
		NextTarget++;
		if (NextTarget == _points.Length)
		{
			NextTarget = 0;
		}
	}

	private void SetPrevTarget()
	{
		NextTarget--;
		if (NextTarget < 0)
		{
			NextTarget = _points.Length - 1;
		}
	}


	private void OnDrawGizmos()
	{
		Vector3 dirWithDistance0 = Vector3.zero;
		Vector3 dirWithDistance1 = Vector3.zero;
		Vector3 dirWithDistance2 = Vector3.zero;

		if (_points != null && _points.Length > 0 && NextTarget < _points.Length)
		{
			dirWithDistance0 = _direction0;
			dirWithDistance1 = _direction1;
			dirWithDistance2 = _direction2;
		}

		// Draw a line and sphere to the position character is facing
		Gizmos.color = Color.red;
		Gizmos.DrawLine(this.transform.position, this.transform.position + dirWithDistance0);
		Gizmos.DrawWireSphere(this.transform.position + dirWithDistance0, 0.25f);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(this.transform.position, this.transform.position + dirWithDistance1);
		Gizmos.DrawWireSphere(this.transform.position + dirWithDistance1, 0.25f);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(this.transform.position, this.transform.position + dirWithDistance2);
		Gizmos.DrawWireSphere(this.transform.position + dirWithDistance2, 0.25f);
	}

}
