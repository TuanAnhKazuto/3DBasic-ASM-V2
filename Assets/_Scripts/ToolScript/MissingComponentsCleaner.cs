#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class MissingComponentsCleaner : MonoBehaviour
{
    [MenuItem("Tools/Remove Missing Scripts")]
    private static void RemoveMissingScripts()
    {
        // Lấy tất cả GameObject trong Scene
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        int removedCount = 0;

        foreach (GameObject obj in allGameObjects)
        {
            // Đếm số lượng component trước khi xóa
            int initialCount = obj.GetComponents<Component>().Length;

            // Sử dụng SerializedObject để đảm bảo xóa toàn bộ
            SerializedObject so = new SerializedObject(obj);
            SerializedProperty components = so.FindProperty("m_Component");

            for (int i = components.arraySize - 1; i >= 0; i--)
            {
                SerializedProperty component = components.GetArrayElementAtIndex(i);
                if (component.objectReferenceValue == null)
                {
                    components.DeleteArrayElementAtIndex(i);
                    removedCount++;
                }
            }

            so.ApplyModifiedProperties();
        }

        Debug.Log($"Đã xóa {removedCount} component bị Missing.");
    }
}
#endif
