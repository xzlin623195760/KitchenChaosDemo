using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;

    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    private void Update() {
        if (KitchenGameManager.Instance.IsGamePlaying() && platesSpawnAmount < platesSpawnAmountMax) {
            spawnPlateTimer += Time.deltaTime;
            if (spawnPlateTimer >= spawnPlateTimerMax) {
                spawnPlateTimer = 0;
                platesSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                //KitchenObject.SpwanKitchenObject(plateKitchenObjectSO, this);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (platesSpawnAmount > 0) {
                platesSpawnAmount--;
                KitchenObject.SpwanKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}