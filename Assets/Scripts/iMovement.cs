using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface iMovement
{
  void Initialize(WaypointManager waypointManager);
  GameObject GetGameObject();

  UnityEvent DeathEvent();
}
