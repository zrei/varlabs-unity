using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_Part1Button;
    [SerializeField] private Button m_Part2Button;

    private const int PART1_BUILD_INDEX = 1;
    private const int PART2_BUILD_INDEX = 2;

    private void Start()
    {
        m_Part1Button.onClick.AddListener(GoToPart1);
        m_Part2Button.onClick.AddListener(GoToPart2);
    }

    private void OnDestroy()
    {
        m_Part1Button.onClick.RemoveAllListeners();
        m_Part2Button.onClick.RemoveAllListeners();
    }

    #region Button Listeners
    private void GoToPart1()
    {
        SceneManager.LoadScene(PART1_BUILD_INDEX);
    }

    private void GoToPart2()
    {
        SceneManager.LoadScene(PART2_BUILD_INDEX);
    }
    #endregion
}
