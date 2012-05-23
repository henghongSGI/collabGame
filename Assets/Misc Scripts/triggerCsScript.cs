using UnityEngine;
using System.Collections;

public class triggerCsScript : Photon.MonoBehaviour {
public float height = 3.2f;
public Texture viewerTexture;
private float speed = 0.5f;
private float timingOffset = 0.0f;
private bool startMove;
public GameObject target;

private Vector3 originPos;	
private Vector3 FinalPos;
	
private float lastFrameTime;
private float thisFrameTime;

private float localLiftTime;	
	
private bool started = false;
	
// Use this for initialization
void Start () {
	startMove = false;
	originPos = target.transform.position;
		
		thisFrameTime = (float)PhotonNetwork.time;
		
}

// Update is called once per frame
void Update () 
{

	
	lastFrameTime = thisFrameTime;
	thisFrameTime = (float)PhotonNetwork.time;
		
	float photonDelta = thisFrameTime - lastFrameTime;
	
		
	if (startMove) 
	{
		localLiftTime += photonDelta;
		
		if (!started)
		{
		
			localLiftTime = 0.0f;
			started = true;		
		}				
		float math = Mathf.Sin(localLiftTime*speed+timingOffset);
		float offset = (1.0f + math )* height / 2.0f;
	  	FinalPos = originPos + new Vector3(0.0f, offset, 0.0f);
//	target.transform.position = FinalPosition;
		if(target.transform.position != FinalPos)
		{
			target.transform.position = Vector3.Lerp(target.transform.position, FinalPos, Time.deltaTime * 2);
		}
		
	}		
		GameObject SpawnManager = GameObject.Find("Code");
		GameManagerVik MoverTest = SpawnManager.GetComponent<GameManagerVik>();
		if(MoverTest.selectedClass == "Viewer")
		{
			this.renderer.material.mainTexture = viewerTexture;
		}
	/*	
	else
	{
		
			if (FinalPos.y > originPos.y)
		{
			localLiftTime += photonDelta;
			float math = Mathf.Sin(localLiftTime*speed+timingOffset);
			float offset = (1.0f + math )* height / 2.0f;
	  	
		  	FinalPos = originPos - new Vector3(0.0f, offset, 0.0f);
		}
		if (FinalPos.y < originPos.y)
		{
			FinalPos = originPos;
		}
	}
*/
	
	
}
/*	else if (!startMove && target.transform.position != originPos) {
		var currentPos = target.transform.position;
		target.transform.position = currentPos - Vector3(0, 0.1, 0);
	}
	else{
		target.transform.position = originPos;
		originPos = target.transform.position;
	}
	*/
void OnTriggerEnter() {
	startMove = true;
}

void OnTriggerExit() {
	startMove = false;
}


}