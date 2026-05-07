using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // 生成された Input Actions クラスを保持する変数 
    private PlayerInputActions _inputActions;

    // 他のスクリプトから入力値を参照するためのプロパティ（読み取り専用） 
    public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        // 1.インスタンスを生成 
        _inputActions = new PlayerInputActions();

        //2.コールバック関数の登録
        // Move アクションの入力値が変化したときに呼び出されるコールバックを設定 
        _inputActions.CharacterControls.Move.started += OnMovementInput;
        _inputActions.CharacterControls.Move.performed += OnMovementInput;
        _inputActions.CharacterControls.Move.canceled += OnMovementInput;
    }

    private void OnEnable()
    {
        // 3.アクションマップを有効化 
        _inputActions.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        // 4.アクションマップを無効化 
        _inputActions.CharacterControls.Disable();
    }

    //入力値を処理するメソッド
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        // 現在の入力値をVector2型で読み取り、変数に格納 
        MovementInput = context.ReadValue<Vector2>();

        // [デバッグ用] 入力値を確認したい場合は以下のコメントを解除してください 
        // Debug.Log($"Movement Input: {MovementInput}"); 
    }

}
