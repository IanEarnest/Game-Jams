using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	
	// Transforms to manipulate objects.
	public Transform sphere; // Sphere.
	public Transform clone;	// Clone of sphere.
	public Transform leftWall;	// Left wall of level.
	public Transform rightWall;	// Right wall of level.
	public Transform capsule;
	
	public float movespeed = 1; // Move speed of gun.
	public int shotPowerStart = 10; // How much power the shot starts with.
	public int shotPower = 10; // Force used to shoot Sphere.
	public int shotPowerPlus = 1; // How fast shot power increaces.
	public int shotPowerLimit = 100; // Max shot power.
	public int frames = 0; // Count for each Update() frame ran.
	private Vector3 gunRotation; // Gun rotation towards mouse.
	public int spheres = 0; // Count for Sphere clones.
	
	// Interface
	public string GUIPower; // Displays shot power.
	public string GUITime; // //Displays frames played so far.
	
	
	// Mouse Position
	Ray ray;
 	RaycastHit hit;
	
	// Graphical User Interface
	void OnGUI() {
		GUI.Box(new Rect(0,0,100,50), GUIPower); // Box displaying shot power at top left of screen.
		GUI.Box(new Rect (Screen.width - 100,0,100,50), GUITime); // //Box displaying total frames at top right of screen.
	}
	
	void Update () {
		// Set ray to mouse position.
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		
		// Controls
		
		// Movement for gun.
		if(Input.GetKey("w"))rigidbody.MovePosition(new Vector3(rigidbody.position.x,rigidbody.position.y+movespeed/5,rigidbody.position.z)); // Press W to go up.
		if(Input.GetKey("a"))rigidbody.MovePosition(new Vector3(rigidbody.position.x-movespeed/5,rigidbody.position.y,rigidbody.position.z)); // Press A to go Left.
		if(Input.GetKey("s"))rigidbody.MovePosition(new Vector3(rigidbody.position.x,rigidbody.position.y-movespeed/5,rigidbody.position.z)); // Press S to go Down.
		if(Input.GetKey("d"))rigidbody.MovePosition(new Vector3(rigidbody.position.x+movespeed/5,rigidbody.position.y,rigidbody.position.z)); // Press D to go Right.
		
		if(spheres < 4){
			// Increase shot power.
			if(Input.GetKey(KeyCode.Mouse0)){ // Check if left mouse button if pressed.
				if(shotPower<shotPowerLimit) // Check if shot power is under limit.
					shotPower += shotPowerPlus; // Increase shot power.
				GUIPower = "Power: " + shotPower; // Display shot power
			}
			
			// Launch Sphere clone.
			if(Input.GetKeyUp(KeyCode.Mouse0)){ // Check if left mouse button is released.
				clone = Instantiate(sphere,transform.position,transform.rotation) as Transform; // Set Clone instance of a Sphere and position at gun.
				clone.rigidbody.AddRelativeForce(0,0,shotPower*20); // Push clone by shot power towards mouse location.
				shotPower = shotPowerStart; // Reset shot power.
			}
		}
		
		// Change timescale of game.
		if(Input.GetKeyDown(KeyCode.Q))Time.timeScale++; // Press Q to increase timescale.
		if(Input.GetKeyDown(KeyCode.E))Time.timeScale--; // Press E to decrease timescale.
		if(Input.GetKeyDown(KeyCode.R))Time.timeScale = 1; // Press R to reset timescale.
		
		
		
		// Restricting movement.
		
		// Restrict movement of gun.
		// If gun hits barriers, stop it moving past.
		//
		
		// Restricting clone of Sphere movement.
		if(clone && clone.position.x > rightWall.position.x || clone && clone.position.x < leftWall.position.x){
			Destroy(clone.gameObject);	// When clone is created and is outside the walls, destroy clone.
		}
		else if(clone && clone.position.x < rightWall.position.x || clone && clone.position.x > leftWall.position.x){
			Destroy(clone.gameObject, 5f);	// When clone is created and inside the walls, destroy clone in 10 seconds.
		}
		
		
		
		// Every frame maintinance.
		
		// Check if raycast has been set, then draw line on Scene Screen.
		if (Physics.Raycast(ray, out hit, 100))
			Debug.DrawLine(transform.position, hit.point); // Draw line from gun to mouse location.
		
		capsule.rigidbody.MovePosition(hit.point); // Set casule at mouse position.
		
		// Rotation of gun towards mouse position.
		gunRotation = new Vector3(hit.point.x,hit.point.y,0);
		transform.LookAt(gunRotation);
		
		spheres = GameObject.FindGameObjectsWithTag("Respawn").Length; // Number of spheres in game.
		GUIPower = "Power: " + shotPower + "\n Spheres: " + spheres; // Display shot power.
		GUITime = "Time: " + (int)Time.time + "\n TimeScale: " + Time.timeScale + "\n Frames: " + frames; // //Display frames run.
		
		frames++; // Add to frames run.
	}
}
