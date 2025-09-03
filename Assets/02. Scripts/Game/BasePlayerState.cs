using UnityEngine;

public abstract class BasePlayerState
{
    public abstract void OnEnter(GameLogic gameLogic);          // 상태 시작
    public abstract void OnExit(GameLogic gameLogic);           // 상태 종료
    public abstract void HandleMove(GameLogic gameLogic, int row, int col); // 마커 표시
    protected abstract void HandleNextTurn(GameLogic gameLogic);    // 턴 전환

    // 게임 결과 처리
    protected void ProcessMove(GameLogic gameLogic, Constants.PlayerType playerType,
        int row, int col)
    {
        if (gameLogic.SetNewBoardValue(playerType, row, col))
        {
            
        }
    }
}