using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Header("移動時間")]
    private float moveTime = 2f;

    [SerializeField]
    [Header("回転時間")]
    private float rotateTime = 2f;

    //正面方向取得用クラス格納用変数
    [SerializeField]
    private ObjectRotation objectRotation;
    public ObjectRotation objRotate => objectRotation;

    //プレイヤー移動クラス
    private PlayerMove playerMove;
    public PlayerMove plMove => playerMove;

    private SkillManager skillManager;
    public SkillManager SkillManager => skillManager;

    //プレイヤーカメラ回転クラス
    private PlayerRotate playerRotate;

    //何かしらのコルーチン中かどうかのフラグ
    public static bool IsNowAction = false;

    private bool isStartMethodEnd = false;
    

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerRotate = GetComponent<PlayerRotate>();
        skillManager = GetComponent<SkillManager>();
    }

    // Start is called before the first frame update
    async void Start()
    {
        //キャンセルトークンの取得
        var token = this.GetCancellationTokenOnDestroy();

        await UniTask.WaitUntil(() => CSVMapGenerate.IsMapGenerate);
        CSVMapGenerate.IsMapGenerate = false;
        //Dictonary Init
        if(objectRotation == null)
        {
            var temp = Camera.main.transform.parent.gameObject;
           objectRotation = temp.transform.Find("RotateManager").GetComponent<ObjectRotation>();
        }
        var vec3s = objectRotation.SetFoward();
        playerMove.SetDirectionDictionary(vec3s[0], vec3s[1], vec3s[2], vec3s[3]);

        playerRotate.PlayerRotateStart(rotateTime);
        playerMove.PlayerMoveStart();
        skillManager.SkillManagerStart();

        ObakeAnimation.Inctance.IdleAnimation();
        isStartMethodEnd = true;
    }

    private void Update()
    {
        if (!isStartMethodEnd) return;
        if (IsNowAction) return;
        ControllerManager.instance.ControllerUpdate();
        playerMove.PLMoveUpdate(moveTime);
        playerMove.PLRotUpdate(moveTime);
        skillManager.SkillManagerUpdate();
    }

}
