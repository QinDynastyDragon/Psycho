using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable{
	float StartHealth { get;}
	float Health { get; set; }
	void ReceivedDamage (float amount);

	//states
	bool IsVulnerable();
}
