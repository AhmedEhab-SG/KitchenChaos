using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs { public BaseCounter selectedCounter; }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private Vector3 lastInteractDir;
    private bool isWalking;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake() {
        if (Instance) Debug.LogError("Multiple Player instances found!");
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteract += GameInput_OnInteract;
        gameInput.OnInteractAlternate += GameInput_OnInteractAlternate;
    }

    private void GameInput_OnInteract(object sender, System.EventArgs e) {
        if (!selectedCounter) return;
        selectedCounter.Interact(this);
    }

    private void GameInput_OnInteractAlternate(object sender, System.EventArgs e) {
        if (!selectedCounter) return;
        selectedCounter.InteractAlternate(this);
    }

    private void Update() {
        HandleCollision();
        HandleInteractions();
    }

    private void HandleInteractions() {
        Vector2 input = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(input.x, 0f, input.y);

        if (moveDir != Vector3.zero) lastInteractDir = moveDir;

        bool isInteract = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance);

        if (isInteract && raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) SetSelectedCounter(baseCounter);
        else SetSelectedCounter(null);
    }

    private void HandleCollision() {
        Vector2 input = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(input.x, 0f, input.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRedius = 0.7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRedius, moveDir, 0.1f);

        if (!canMove) {
            // Check if we can move on X
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;

            bool canMoveX = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRedius, moveDirX, moveDistance);

            // If we can move on X, move on X
            if (canMoveX) transform.position += moveDirX * moveDistance;
            else {
                // Check if we can move on Z
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;

                bool canMoveZ = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRedius, moveDirZ, moveDistance);

                // If we can move on Z, move on Z
                if (canMoveZ) transform.position += moveDirZ * moveDistance;
            }
        }

        if (canMove) transform.position += moveDir * moveDistance;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }

    public Transform GetKitchenObjectTransform() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
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