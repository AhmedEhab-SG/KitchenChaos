using UnityEngine;

public interface IKitchenObjectParent {
    public Transform GetKitchenObjectTransform();
    public void SetKitchenObject(KitchenObject kichenObject);
    public KitchenObject GetKitchenObject();
    public void ClearKitchenObject();
    public bool HasKitchenObject();
}