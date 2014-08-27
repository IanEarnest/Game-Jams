using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	
	// Transforms to manipulate objects.
	public Transform sphere; // Sphere.
	private Transform clone;	// Clone of sphere.
	public Transform targetSphere; // Target spheres.
	public Transform wallLeft;	// Left wall of level.
	public Transform wallRight;	// Right wall of level.
	public Transform wallTop;	// Top wall of level.
	public Transform wallDown;	// Down wall of level.
	public Transform capsule;
	public Transform cameraMain;
	public Transform cameraWide;
	
	public float movespeed = 1; // Move speed of gun.
	public int shotPowerStart = 10; // How much power the shot starts with.
	public int shotPower = 10; // Force used to shoot Sphere.
	public int shotPowerPlus = 1; // How fast shot power increaces.
	public int shotPowerLimit = 100; // Max shot power.
	public int frames = 0; // Count for each Update() frame ran.
	private Vector3 gunRotation; // Gun rotation towards mouse.
	public int spheres = 0; // Count for Sphere clones.
	public int maxSpheres = 5;
	public int targetSpheres = 0;
	public float targetOrigin = 0;
	public int scrollLimit = 2;
	public int scrolled = 0;
	
	// Interface
	public string GUIPower; // Displays shot power.
	public string GUITime; // //Displays frames played so far.
	
	
	// Mouse Position
	Ray ray;
 	RaycastHit hit;
	
	// Graphical User Interface
	void OnGUI() {
		// Box displaying shot power at top left of screen.
		GUI.Box(new Rect(0,0,100,50), GUIPower); 
		//Box displaying total frames and time at top right of screen.
		GUI.Box(new Rect (Screen.width - 100,0,100,50), GUITime);
	}
	
	string hitChecker = "Nothing";
	
	void Update () {
		// Set ray to mouse position.
		if(cameraMain.camera.enabled == true)
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		else
			ray = cameraWide.camera.ScreenPointToRay(Input.mousePosition);
		
		// Hitting target.
		if(targetOrigin != 0 && targetSphere)
		{
			if(hitChecker == "Nothing")
			if(targetSphere.transform.position.x == targetOrigin) {
				Debug.Log("Not hit");
				hitChecker = "Not Hit";
			}
			if(hitChecker == "Nothing" || hitChecker == "Not Hit")
			if(targetSphere.transform.position.x != targetOrigin) {
				Debug.Log("Hit");
				Destroy(targetSphere.gameObject,2f);
				hitChecker = "Hit";
			}
		}
		
		
		// Controls
		
		// Movement for gun.
		// Press W to go up.
		if(Input.GetKey("w"))
			rigidbody.MovePosition(new Vector3(rigidbody.position.x
											  ,rigidbody.position.y+movespeed/5
											  ,rigidbody.position.z));
		// Press A to go Left.
		if(Input.GetKey("a"))
			rigidbody.MovePosition(new Vector3(rigidbody.position.x-movespeed/5
											  ,rigidbody.position.y
											  ,rigidbody.position.z));
		// Press S to go Down.
		if(Input.GetKey("s"))
			rigidbody.MovePosition(new Vector3(rigidbody.position.x
										      ,rigidbody.position.y-movespeed/5
											  ,rigidbody.position.z));
		// Press D to go Right.
		if(Input.GetKey("d"))
			rigidbody.MovePosition(new Vector3(rigidbody.position.x+movespeed/5
											  ,rigidbody.position.y
											  ,rigidbody.position.z));
		
		// Limit Spheres created.
		if(spheres < maxSpheres){
			// Increase shot power.
			// Check if left mouse button if pressed.
			if(Input.GetKey(KeyCode.Mouse0)){
				// Check if shot power is under limit.
				if(shotPower<shotPowerLimit)
					// Increase shot power.
					shotPower += shotPowerPlus;
				// Display shot power
				GUIPower = "Power: " + shotPower;
			}
			
			// Launch Sphere clone.
			// Check if left mouse button is released.
			if(Input.GetKeyUp(KeyCode.Mouse0)){ 
				// Set Clone instance of a Sphere and position at gun.
				clone = Instantiate(sphere
								   ,transform.position
								   ,transform.rotation)
									as Transform;
				// Push clone by shot power towards mouse location.
				clone.rigidbody.AddRelativeForce(0
												,0
												,shotPower*20);
				// Reset shot power.
				shotPower = shotPowerStart;
			}
		}
		
		
		float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
		
		
		if(scrolled < scrollLimit){
			// Mouse scroll forwards.
			if(scrollWheel > 0){
				// Move camera forward.
				cameraMain.Translate(Vector3.forward*5);
				scrolled++;
			}
		}
		if(scrolled > ~scrollLimit){ // ~ means reverse.
			// Mouse scroll backwards.
			if(scrollWheel < 0){
				// Move camera back.
				cameraMain.Translate(Vector3.back*5);
				scrolled--;
			}
		}
			
		// Change timescale of game.
		// Press Q to increase timescale.
		if(Input.GetKeyDown(KeyCode.Q))
			Time.timeScale++;
		// Press E to decrease timescale.
		if(Input.GetKeyDown(KeyCode.E))
			Time.timeScale--;
		// Press R to reset timescale.
		if(Input.GetKeyDown(KeyCode.R))
			Time.timeScale = 1;
		
		
		
		// Restricting movement.
		
		// Restrict movement of gun.
		if(transform.position.x > wallRight.position.x){
			rigidbody.MovePosition(new Vector3(wallLeft.position.x
											  ,rigidbody.position.y
											  ,rigidbody.position.z));
		}
		if(transform.position.x < wallLeft.position.x){
			rigidbody.MovePosition(new Vector3(wallRight.position.x
											  ,rigidbody.position.y
											  ,rigidbody.position.z));
		}
		if(transform.position.y < wallDown .position.y){
			rigidbody.MovePosition(new Vector3(rigidbody.position.x
											  ,wallTop.position.y
											  ,rigidbody.position.z));
		}
		if(transform.position.y > wallTop.position.y){
			rigidbody.MovePosition(new Vector3(rigidbody.position.x
											  ,wallDown.position.y
											  ,rigidbody.position.z));
		}		
		
		string clonePosition = "";
		// Restricting clone of Sphere movement.
		if(clone){
			if(clone.position.x > wallRight.position.x || clone.position.x < wallLeft.position.x
			|| clone.position.y < wallDown .position.y || clone.position.y > wallTop.position.y){
				clonePosition = "Outside";
			}
			else if(clone.position.x < wallRight.position.x || clone.position.x > wallLeft.position.x
				 || clone.position.y > wallDown .position.y || clone.position.y < wallTop.position.y){
				clonePosition = "Inside";
			}
		}
		if(clonePosition == "Outside"){
			// When clone is created and is outside the walls, destroy clone.
			Destroy(clone.gameObject);
		}
		if(clonePosition == "Inside"){
			// When clone is created and inside the walls, destroy clone in 10 seconds.
			Destroy(clone.gameObject, 5f);
		}
		
		// Change cameras
		// Press C to invert to other camera.
		if(Input.GetKeyDown(KeyCode.C)) {
			cameraMain.camera.enabled = !cameraMain.camera.enabled;
			cameraWide.camera.enabled = !cameraWide.camera.enabled;
		}
		
		
		
		// Every frame maintinance.
				
		// Check if raycast has been set, then draw line on Scene Screen.
		if (Physics.Raycast(ray, out hit, 100))
			Debug.DrawLine(transform.position, hit.point); // Draw line from gun to mouse location.
		
		capsule.rigidbody.MovePosition(hit.point); // Set casule at mouse position.
		
		Vector3 cameraPosition = new Vector3(transform.position.x-cameraMain.transform.position.x
											,transform.position.y-cameraMain.transform.position.y
											,0);
		cameraMain.Translate(cameraPosition); // Position camera above Gun.
		
		// Rotation of gun towards mouse position.
		gunRotation = new Vector3(hit.point.x,hit.point.y,0);
		transform.LookAt(gunRotation);
		
		if(targetSphere)targetOrigin = targetSphere.transform.position.x;
		
		capsule.Rotate(new Vector3(0,0,10));
		
		spheres = GameObject.FindGameObjectsWithTag("Respawn").Length; // Number of spheres in game.
		targetSpheres = GameObject.FindGameObjectsWithTag("Finish").Length; // Number of target spheres in game.
		GUIPower = "Power: " + shotPower + "\n Spheres: " + spheres + "\n Targets: " + targetSpheres; // Display shot power, spheres and target spheres.
		GUITime = "Time: " + (int)Time.time + "\n TimeScale: " + Time.timeScale + "\n Frames: " + frames; // //Display frames run.
		
		frames++; // Add to frames run.
		
		// Game Over.
		if(GameObject.FindGameObjectsWithTag("Finish").Length == 0){
			Debug.Log("Game Over.");
		}
	}
}
