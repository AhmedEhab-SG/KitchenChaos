using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // to put on the counter
            if (player.HasKitchenObject()) player.GetKitchenObject().SetKitchenObjectParent(this);
        } else {
            // to take from the counter

            // if player holds smth
            if (player.HasKitchenObject()) {

                // if player hodling plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {

                    // if the plate can be add on it 
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        GetKitchenObject().DestorySelf();
                } else {
                    // player hodling smth else than plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // counter holds plate

                        // if the plate can be add on it
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                            player.GetKitchenObject().DestorySelf();
                    }
                }
            } else GetKitchenObject().SetKitchenObjectParent(player);

        }
    }
}

