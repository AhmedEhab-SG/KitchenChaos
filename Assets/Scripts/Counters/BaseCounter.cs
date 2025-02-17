using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private Transform counterTopPoint;

    public static event EventHandler OnAnyObjectPlaced;

    private KitchenObject kitchenObject;

    public abstract void Interact(Player player);

    public virtual void InteractAlternate(Player player) { }

    public Transform GetKitchenObjectTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject) OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
