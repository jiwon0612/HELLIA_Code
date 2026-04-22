using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins;
using UnityEngine;
using UnityEngine.Serialization;

public class StageManager : MonoBehaviour
{
   public static StageManager instance;
   [SerializeField] private Player _player;
   [SerializeField] private CameraManager _cameraManager;

   [field:SerializeField]public int CurrentStageIdx { get; private set; } = 0;
   
   public List<CheckPoint> checkPoints = new List<CheckPoint>();
   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.O))
      {
         InitPosition(_player);
      }
   }


   public void NextStageIdx()
   {
      CurrentStageIdx++;
   }

   public void InitPosition(Player _player)
   {
      _player.transform.position = checkPoints[CurrentStageIdx].transform.position;
      checkPoints[CurrentStageIdx].InitObjects();
      _player.Initialize();

      _cameraManager.BlendTime = 0.8f;
   }
}
