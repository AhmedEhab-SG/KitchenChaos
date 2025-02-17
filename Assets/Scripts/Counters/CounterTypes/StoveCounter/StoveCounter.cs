using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
    public event EventHandler<IHasProgress.OnProcessChangedEventArgs> OnProcessChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;

    }

    public enum State {
        Empty,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeArr;
    [SerializeField] private BurnedRecipeSO[] burnedRecipeArr;

    private State state;
    private float fryingTime;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTime;
    private BurnedRecipeSO burnedRecipeSO;


    private void Start() {
        state = State.Empty;
    }

    private void Update() {
        if (!HasKitchenObject()) return;

        switch (state) {
            case State.Empty:
                break;
            case State.Frying:
                fryingTime += Time.deltaTime;

                OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArgs { progressNormalized = fryingTime / fryingRecipeSO.fryingTimerMax });

                //Fried
                if (fryingTime >= fryingRecipeSO.fryingTimerMax) {
                    GetKitchenObject().DestorySelf();

                    KitchenObject.CreateKitchenObject(fryingRecipeSO.output, this);

                    state = State.Fried;
                    burningTime = 0;
                    burnedRecipeSO = GetBurnedResicpeSOWithInput(fryingRecipeSO.output);

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                }
                break;

            case State.Fried:
                burningTime += Time.deltaTime;

                OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArgs { progressNormalized = burningTime / burnedRecipeSO.burnedTimerMax });


                //Burned
                if (burningTime >= burnedRecipeSO.burnedTimerMax) {
                    GetKitchenObject().DestorySelf();

                    KitchenObject.CreateKitchenObject(burnedRecipeSO.output, this);

                    state = State.Burned;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                    OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArgs { progressNormalized = 0f });

                }
                break;

            case State.Burned:
                break;
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // to put on the counter and check if the recipe is has input
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                player.GetKitchenObject().SetKitchenObjectParent(this);

                fryingRecipeSO = GetFryingResicpeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                state = State.Frying;
                fryingTime = 0;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
            }
        } else { // to take from the counter
            if (player.HasKitchenObject()) {
                // if player holds smth
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {

                    // if player hodling plate 
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestorySelf();

                        // clear the state
                        state = State.Empty;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                        OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArgs { progressNormalized = 0f });
                    }
                }
            } else {
                // player does not have kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Empty;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

                OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArgs { progressNormalized = 0f });
            }
        }
    }

    // validate if the recipe has input
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO) {
        return GetFryingResicpeSOWithInput(kitchenObjectSO) != null;
    }

    private FryingRecipeSO GetFryingResicpeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeArr)
            if (fryingRecipeSO.input == inputKitchenObjectSO) return fryingRecipeSO;

        return null;
    }

    private BurnedRecipeSO GetBurnedResicpeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurnedRecipeSO burnedRecipeSO in burnedRecipeArr)
            if (burnedRecipeSO.input == inputKitchenObjectSO) return burnedRecipeSO;

        return null;
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingResicpeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null ? fryingRecipeSO.output : null;
    }
}
