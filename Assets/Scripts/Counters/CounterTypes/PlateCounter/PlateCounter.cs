using System;
using UnityEngine;

public class PlateCounter : BaseCounter {
    public event EventHandler OnPlateSpwaned;

    public event EventHandler OnPlateTaken;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spwanPlateTime;
    private float spwanPlateTimeMax = 5f;
    private int platesSpwanedAmount = 0;
    private int platesSpwanedAmountMax = 4;

    public void Update() {
        spwanPlateTime += Time.deltaTime;

        if (spwanPlateTime >= spwanPlateTimeMax) {
            spwanPlateTime = 0f;

            if (platesSpwanedAmount < platesSpwanedAmountMax) {
                platesSpwanedAmount++;

                OnPlateSpwaned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject() || platesSpwanedAmount <= 0) return;

        platesSpwanedAmount--;

        KitchenObject.CreateKitchenObject(plateKitchenObjectSO, player);

        OnPlateTaken?.Invoke(this, EventArgs.Empty);
    }
}
