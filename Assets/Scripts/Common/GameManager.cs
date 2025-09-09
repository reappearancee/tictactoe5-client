using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;           // 확인 패널
    [SerializeField] private GameObject signinPanel;            // 로그인 패널
    [SerializeField] private GameObject signupPanel;            // 회원가입 패널
    
    // Main Scene에서 선택한 게임 타입
    private Constants.GameType _gameType;
    
    // Panel을 띄우기 위한 Canvas 정보
    private Canvas _canvas;
    
    // Game Logic
    private GameLogic _gameLogic;
    
    // Game 씬의 UI를 담당하는 객체
    private GameUIController _gameUIController;

    private void Start()
    {
        // 로그인
        var sid = PlayerPrefs.GetString("sid");
        if (string.IsNullOrEmpty(sid))
        {
            OpenSigninPanel();
        }
    }

    /// <summary>
    /// Main에서 Game Scene으로 전환시 호출될 메서드
    /// </summary>
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Game에서 Main Scene으로 전환시 호출될 메서드
    /// </summary>
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Confirm Panel을 띄우는 메서드
    /// </summary>
    /// <param name="message"></param>
    public void OpenConfirmPanel(string message, 
        ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>()
                .Show(message, onConfirmButtonClicked);
        }
    }

    /// <summary>
    /// 로그인 팝업 표시
    /// </summary>
    public void OpenSigninPanel()
    {
        if (_canvas != null)
        {
            var signinPanelObject = Instantiate(signinPanel, _canvas.transform);
            signinPanelObject.GetComponent<SigninPanelController>().Show();
        }
    }

    public void OpenSignupPanel()
    {
        if (_canvas != null)
        {
            var signupPanelObject = Instantiate(signupPanel, _canvas.transform);
            signupPanelObject.GetComponent<SignupPanelController>().Show();
        }
    }

    /// <summary>
    /// Game Scene에서 턴을 표시하는 UI를 제어하는 함수
    /// </summary>
    /// <param name="gameTurnPanelType">표시할 Turn 정보</param>
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType gameTurnPanelType)
    {
        _gameUIController.SetGameTurnPanel(gameTurnPanelType);
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            // Block 초기화
            var blockController = FindFirstObjectByType<BlockController>();
            if (blockController != null)
            {
                blockController.InitBlocks();
            }

            // Game UI Controller 할당 및 초기화
            _gameUIController = FindFirstObjectByType<GameUIController>();
            if (_gameUIController != null)
            {
                _gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }
            
            // GameLogic 생성
            _gameLogic = new GameLogic(blockController, _gameType);
        }
    }
}
