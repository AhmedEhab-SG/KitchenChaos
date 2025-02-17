using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IHasProgress {
    public event EventHandler<IHasProgress.OnProcessChangedEventArgs> OnProcessChanged;

    // make a new event the onProcessChanged will fire at zero
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArr;

    private int cuttingProcess;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // to put on the counter and check if the recipe is has input
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                player.GetKitchenObject().SetKitchenObjectParent(this);

                cuttingProcess = 0;

                // get the cutting process max from the recipe and pass it to the progress bar
                OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArgs { progressNormalized = (float)cuttingProcess / GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO()).cuttingProcessMax });
            }

        } else {   // to take from the counter
            if (player.HasKitchenObject()) {
                // if player holds smth
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {

                    // if player hodling plate 
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestorySelf();
                    }
                }

            } else GetKitchenObject().SetKitchenObjectParent(player);
        }
    }

    public override void InteractAlternate(Player player) {
        // check if the kitchenObj can be cut and there is Object on the counter
        if (!HasKitchenObject() || !HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) return;

        cuttingProcess++;

        OnCut?.Invoke(this, EventArgs.Empty);

        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

        OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArgs { progressNormalized = (float)cuttingProcess / cuttingRecipeSO.cuttingProcessMax });

        // check if the cutting process is done
        if (cuttingProcess >= cuttingRecipeSO.cuttingProcessMax) Cut();
    }

    private void Cut() {
        // Get the output from the input
        KitchenObjectSO outputKitchenObjectSO = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());

        GetKitchenObject().DestorySelf();

        // Create a new kitchen object
        KitchenObject.CreateKitchenObject(outputKitchenObjectSO, this);
    }

    // validate if the recipe has input
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO) {
        return GetCuttingRecipeSOWithInput(kitchenObjectSO) != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArr)
            if (cuttingRecipeSO.input == inputKitchenObjectSO) return cuttingRecipeSO;

        return null;
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null ? cuttingRecipeSO.output : null;
    }
}
