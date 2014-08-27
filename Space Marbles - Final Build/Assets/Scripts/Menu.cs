using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public TextMesh title;
	public TextMesh play;
	public TextMesh levelSelect;
	public TextMesh options;
	public TextMesh quit;
	
	public TextMesh maxSpheres;
	public TextMesh maxSpheresValue;
	public TextMesh maxSpheresPlus;
	public TextMesh maxSpheresMinus;
	public TextMesh moveSpeed;
	public TextMesh moveSpeedValue;
	public TextMesh moveSpeedPlus;
	public TextMesh moveSpeedMinus;
	public TextMesh optionsClose;
	
	public TextMesh level1;
	public TextMesh level2;
	public TextMesh level3;
	public TextMesh level4;
	public TextMesh levelClose;
	
	int shootingMaxSpheres;
	float shootingMoveSpeed;
	int oMaxSpheres;
	float oMoveSpeed;

	Ray ray;
	RaycastHit hit;
	float charSize;
	
	void Start () {
		charSize = title.characterSize;
	}	
	
	void Update () {
		maxSpheresValue.text = "" + shootingMaxSpheres;
		moveSpeedValue.text =  "" + shootingMoveSpeed;
		
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)){
			
			if(hit.transform.name == "Title"){
				title.fontStyle = FontStyle.BoldAndItalic;
				
				if(Input.GetMouseButtonDown(0)){
					if(title.characterSize < charSize+1)
						title.characterSize++;
				}
			}
			
			if(hit.transform.name == "Play"){
				play.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					Application.LoadLevel(1);
			}
			if(hit.transform.name == "Level Select"){
				levelSelect.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0)){
					level1.renderer.enabled = !level1.renderer.enabled;
					level2.renderer.enabled = !level2.renderer.enabled;
					level3.renderer.enabled = !level3.renderer.enabled;
					level4.renderer.enabled = !level4.renderer.enabled;
					levelClose.renderer.enabled = !levelClose.renderer.enabled;
					
					maxSpheres.renderer.enabled = false;
					maxSpheresValue.renderer.enabled = false;
					maxSpheresPlus.renderer.enabled = false;
					maxSpheresMinus.renderer.enabled = false;
					moveSpeed.renderer.enabled = false;
					moveSpeedValue.renderer.enabled = false;
					moveSpeedPlus.renderer.enabled = false;
					moveSpeedMinus.renderer.enabled = false;
					optionsClose.renderer.enabled = false;	
				}
			}
			if(hit.transform.name == "Options"){
				options.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0)){
					maxSpheres.renderer.enabled = !maxSpheres.renderer.enabled;
					maxSpheresValue.renderer.enabled = !maxSpheresValue.renderer.enabled;
					maxSpheresPlus.renderer.enabled = !maxSpheresPlus.renderer.enabled;
					maxSpheresMinus.renderer.enabled = !maxSpheresMinus.renderer.enabled;
					moveSpeed.renderer.enabled = !moveSpeed.renderer.enabled;
					moveSpeedValue.renderer.enabled = !moveSpeedValue.renderer.enabled;
					moveSpeedPlus.renderer.enabled = !moveSpeedPlus.renderer.enabled;
					moveSpeedMinus.renderer.enabled = !moveSpeedMinus.renderer.enabled;
					optionsClose.renderer.enabled = !optionsClose.renderer.enabled;
					
					level1.renderer.enabled = false;
					level2.renderer.enabled = false;
					level3.renderer.enabled = false;
					level4.renderer.enabled = false;
					levelClose.renderer.enabled = false;
				}
			}
			if(hit.transform.name == "Quit"){
				quit.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					Application.Quit();
			}
			
			
			if(hit.transform.name == "Max Spheres"){
				maxSpheres.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					shootingMaxSpheres = oMaxSpheres;
			}
			if(hit.transform.name == "Max Spheres Plus"){
				maxSpheresPlus.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					shootingMaxSpheres++;
			}
			if(hit.transform.name == "Max Spheres Minus"){
				maxSpheresMinus.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					shootingMaxSpheres--;
			}
			if(hit.transform.name == "Move Speed"){
				moveSpeed.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					shootingMoveSpeed = oMoveSpeed;
			}
			if(hit.transform.name == "Move Speed Plus"){
				moveSpeedPlus.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					shootingMoveSpeed++;
			}
			if(hit.transform.name == "Move Speed Minus"){
				moveSpeedMinus.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					shootingMoveSpeed--;
			}
			if(hit.transform.name == "Options Close"){
				optionsClose.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0)){
					maxSpheres.renderer.enabled = !maxSpheres.renderer.enabled;
					maxSpheresValue.renderer.enabled = !maxSpheresValue.renderer.enabled;
					maxSpheresPlus.renderer.enabled = !maxSpheresPlus.renderer.enabled;
					maxSpheresMinus.renderer.enabled = !maxSpheresMinus.renderer.enabled;
					moveSpeed.renderer.enabled = !moveSpeed.renderer.enabled;
					moveSpeedValue.renderer.enabled = !moveSpeedValue.renderer.enabled;
					moveSpeedPlus.renderer.enabled = !moveSpeedPlus.renderer.enabled;
					moveSpeedMinus.renderer.enabled = !moveSpeedMinus.renderer.enabled;
					optionsClose.renderer.enabled = !optionsClose.renderer.enabled;
				}
			}
			
			
			if(hit.transform.name == "Level 1"){
				level1.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					Application.LoadLevel(1);
			}
			if(hit.transform.name == "Level 2"){
				level2.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					Application.LoadLevel(2);
			}
			if(hit.transform.name == "Level 3"){
				level3.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					Application.LoadLevel(3);
			}
			if(hit.transform.name == "Level 4"){
				level4.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0))
					Application.LoadLevel(4);
			}
			if(hit.transform.name == "Level Select Close"){
				levelClose.fontStyle = FontStyle.Bold;
				if(Input.GetMouseButtonDown(0)){
					level1.renderer.enabled = !level1.renderer.enabled;
					level2.renderer.enabled = !level2.renderer.enabled;
					level3.renderer.enabled = !level3.renderer.enabled;
					level4.renderer.enabled = !level4.renderer.enabled;
					levelClose.renderer.enabled = !levelClose.renderer.enabled;
				}
			}
		}
		else
		{
			title.fontStyle = FontStyle.Bold;
			play.fontStyle = FontStyle.Normal;
			levelSelect.fontStyle = FontStyle.Normal;
			options.fontStyle = FontStyle.Normal;
			quit.fontStyle = FontStyle.Normal;
			
			maxSpheres.fontStyle = FontStyle.Normal;
			maxSpheresPlus.fontStyle = FontStyle.Normal;
			maxSpheresMinus.fontStyle = FontStyle.Normal;
			moveSpeed.fontStyle = FontStyle.Normal;
			moveSpeedPlus.fontStyle = FontStyle.Normal;
			moveSpeedMinus.fontStyle = FontStyle.Normal;
			optionsClose.fontStyle = FontStyle.Normal;
			
			level1.fontStyle = FontStyle.Normal;
			level2.fontStyle = FontStyle.Normal;
			level3.fontStyle = FontStyle.Normal;
			level4.fontStyle = FontStyle.Normal;
			levelClose.fontStyle = FontStyle.Normal;
			title.characterSize = charSize;			
		}
	}
}
