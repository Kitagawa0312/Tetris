using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BoardView boardView = default;

    private BoardModel boardModel = default;
    private BoardPresenter boardPresenter = default;

    private void Start()
    {
        // Model¶¬
        boardModel = new BoardModel();

        // Presenter¶¬
        boardPresenter = new BoardPresenter(boardModel);

        // View‰Šú‰»
        boardView.Initialize();
    }
}