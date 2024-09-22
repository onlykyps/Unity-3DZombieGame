using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	Camera cam;
	public LayerMask movementMask;
	PlayerMotor motor;
	public Interactable focus;

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
	}

	// Update is called once per frame
	void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, movementMask))
			{
				//Debug.Log("We hit " + hit.collider.name + " at " + hit.point);
				motor.MoveToPoint(hit.point);
				//stop focusing any objects
				RemoveFocus();
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				// Check if we hit an interactable
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				// If yes set as focus
				if (interactable != null)
				{
					SetFocus(interactable);
				}
			}
		}

	}

	private void RemoveFocus()
	{
		if (focus != null)
		{
			focus.OnDefocused();
		}

		focus = null;
		motor.StopFollowingTarget();
	}

	private void SetFocus(Interactable newFocus)
	{
		if (newFocus != focus)
		{
			if (focus != null)
			{
				focus.OnDefocused();
			}

			focus = newFocus;
			motor.FollowTarget(newFocus);
		}
		newFocus.OnFocused(transform);
	}
}
