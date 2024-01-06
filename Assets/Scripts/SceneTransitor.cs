using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitor : Singletone<SceneTransitor>
{
    [SerializeField] int _testId;
    public int TestId => _testId;
    [SerializeField] string testSceneName;

    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void OpenTest(int testId)
    {
        _testId = testId;
        OpenScene("TestReader");
    }
}
