/**
 *
 * TransformPositionOnSingleAxis Script
 * Created by Luke A Chase - chase.luke.a@gmail.com
 *
 * -------------------------------------------------------------
 * Applying this script to a game object will allow the object
 * to move between two points on a signle axis.
 * -------------------------------------------------------------
 *
 * Editor Fields:
 * Move Axis      - the axis to move on
 * Move Amount    - the amount (in meters) to move
 * Move Speed     - the speed to move (based on fps)
 * Start Position - the position the object should start at:
 *                  Origin: initial position in the editor
 *                  Destination: the final position traveled to
 * Auto Moves     - the object will automatically move from start
 *                  to end point on the specified axis
 * Enabled        - the movement of the object is/isn't allowed
 *                  (i.e. use as a switch - turn it on or off)
 *
 * -------------------------------------------------------------
 * Use this in conjunction with the GameUtility.cs class for the
 * new data types; Trigger this script with the Trigger.cs class
 * -------------------------------------------------------------
*/

using System.Collections;
using UnityEngine;

public class TransformPositionOnSingleAxis : MonoBehaviour
{
	[SerializeField] private Axis moveAxis;
	[SerializeField] private float moveAmount;
	[SerializeField] private float moveSpeed;
	[SerializeField] private MoveTo startPosition;
	[SerializeField] private bool autoMoves;
	[SerializeField] bool enabled;

	private Vector3 originPosition;
	private Vector3 currentPosition;
	private Vector3 destinationPosition;

	private MoveTo moveToPosition;
	private Direction moveDirection;
	private float movedAmount;

	private bool toggled;


	void Start ()
	{
		movedAmount = 0;
		toggled = false;

		if (startPosition == MoveTo.Origin)
		{
			moveToPosition = MoveTo.Destination;
			originPosition = transform.position;
			currentPosition = originPosition;
			destinationPosition = newPosition (originPosition, moveAxis, moveAmount);
		}
		else // startPosition is MoveTo.Destination
		{
			moveToPosition = MoveTo.Origin;
			originPosition = newPosition (transform.position, moveAxis, moveAmount);
			currentPosition = originPosition;
			destinationPosition = transform.position;

			transform.position = newPosition (moveToPosition);
		}
	}


	void Update ()
	{
		if ((autoMoves == true || toggled == true) && enabled)
		{
			move ();
		}
	}


	// when Toggle() is called, object moves until the destination
    // has been reached, at which point the object will stop moving
	public void Toggle ()
	{
		if (enabled)
		{
			toggled = true;
		}
	}


	public void Enable ()
	{
		enabled = !enabled;
	}


	void move()
	{
		currentPosition = newPosition (currentPosition, moveAxis, newMoveAmount());

		if (GameUtility.unsignedValue(movedAmount) >= GameUtility.unsignedValue(moveAmount))
		{
			movedAmount = 0;
			toggled = false;
			transform.position = newPosition (moveToPosition);
		}
		else
		{
			transform.position = currentPosition;
		}
	}


	// for setting the objects inital start, or restart, position
	Vector3 newPosition(MoveTo newPosition)
	{
		if (newPosition == MoveTo.Origin)
		{
			moveToPosition = MoveTo.Destination;
			return originPosition;
		}
		else
		{
			moveToPosition = MoveTo.Origin;
			return destinationPosition;
		}
	}


	// for setting single axis position
	Vector3 newPosition(Vector3 currentPosition, Axis axis, float amount)
	{
		float x = currentPosition.x;
		float y = currentPosition.y;
		float z = currentPosition.z;

		switch (axis)
		{
		case Axis.x:
			return new Vector3 (x + amount, y, z);
		case Axis.y:
			return new Vector3 (x, y + amount, z);
		case Axis.z:
			return new Vector3 (x, y, z + amount);
		default:
			return currentPosition;
		}
	}


	float newMoveAmount()
	{
		moveDirection = newMoveDirection ();

		float newMoveAmount = Time.deltaTime * moveSpeed * (int)moveDirection / 60;

		movedAmount += newMoveAmount;

		// check if current point has moved past the destination point, set final amount to stop on moveToPosition
		if (GameUtility.unsignedValue(movedAmount) >= GameUtility.unsignedValue(moveAmount))
		{
			newMoveAmount = (GameUtility.unsignedValue(movedAmount) - GameUtility.unsignedValue(moveAmount)) * (int)moveDirection;
		}

		return newMoveAmount;
	}


	Direction newMoveDirection()
	{
		float originPoint = getAxisPoint (originPosition, moveAxis);
		float currentPoint = getAxisPoint (transform.position, moveAxis);
		float destinationPoint = getAxisPoint (destinationPosition, moveAxis);

		if (currentPoint == originPoint && originPoint < destinationPoint)
		{
			return Direction.Positive;
		}
		else if (currentPoint == originPoint && originPoint > destinationPoint)
		{
			return Direction.Negative;
		}
		else if (currentPoint == destinationPoint && destinationPoint < originPoint)
		{
			return Direction.Positive;
		}
		else if (currentPoint == destinationPoint && destinationPoint > originPoint)
		{
			return Direction.Negative;
		}
		else
		{
			return moveDirection;  // keep moving the same direction
		}
	}


	float getAxisPoint(Vector3 position, Axis axis)
	{
		switch (axis)
		{
		case Axis.x:
			return position.x;
		case Axis.y:
			return position.y;
		case Axis.z:
			return position.z;
		default:
			return 0;
		}
	}

}
