using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{
    // 48 rows
    public const int SPRITE_ROWS = 48;
    public const int FLIP_OFFSET = SPRITE_ROWS/2;
    public const int SPRITE_COLS = 6;
    public const float ANGLES_PER_ROW = 360f / SPRITE_ROWS;
    public const int IDLE = 0;
    public const int CROUCH = 1;
    public const int JUMP = 4;
    public const int RUN_START = 2;
    public const int RUN_FRAMES = 4;
    public const float EPSILON = 0.1f;

    [SerializeField] private CharacterController2D _characterController;
    [SerializeField] private CharacterAimController _characterAimController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _spriteSheet;
    [SerializeField] private int FPS = 6;
    [SerializeField] private float _animTime = -1;

    public void Update()
    {
        int row = GetRow(_characterAimController.CrossHairPosition, _characterController.IsFlipped);
        int col = _characterController.IsCrouching ? CROUCH : IDLE;
        if (_characterController.Speed <= EPSILON)
        {
            _animTime = 0;
        }
        else
        {
            _animTime += Time.deltaTime;
            col = GetRunColumn(_animTime, FPS);
        }
        
        
        col = _characterController.IsGrounded ? col : JUMP;
        _spriteRenderer.sprite = _spriteSheet.Get(row, col);
    }

    public static int GetRunColumn(float time, int fps)
    {
        int frame = (((int)(time * fps)) % RUN_FRAMES) + RUN_START;
        return frame;
    }


    public static int GetRow(Vector2 aiming, bool flipped)
    {

        float angle = Mathf.Atan2(aiming.y, aiming.x) * Mathf.Rad2Deg;
        angle = angle >= 0 ? angle : angle + 360;
        // Mirror angle
        angle = !flipped ? angle : (540f - angle) % 360f;
        int row = Mathf.RoundToInt(angle / ANGLES_PER_ROW) % 48;
        return row;
    }


}

public static class CharacterSpriteControllerHelpers
{
    public static Sprite Get(this Sprite[] sheet, int row, int column)
    {
        return sheet[row * CharacterSpriteController.SPRITE_COLS + column];
    }
}