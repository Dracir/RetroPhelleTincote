using UnityEngine;
using System.Collections;

public class MachineActivator : MonoBehaviour {

	MachineWind machinWind;
	float cooldown;
	public float cooldownTime = 1f;
	
	void Start () {
		machinWind = transform.parent.gameObject.GetComponent<MachineWind>();
	}
	
	void Update(){
		if(cooldown > 0){
			cooldown -= Time.deltaTime;
			if(cooldown < 0) cooldown = 0;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(cooldown > 0) return;
		if(other.tag == "Player"){
			if(machinWind.StateIsActive<MachineWindIdle>()){
				machinWind.SwitchState<MachineWindBlow>();
			}else{
				machinWind.SwitchState<MachineWindIdle>();
			}
			cooldown = cooldownTime;
		}
    }
}
