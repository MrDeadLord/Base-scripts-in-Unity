///Made by https://github.com/MrDeadLord
///Any questions/suggestions: https://stackoverflow.com/users/13863823/dead-lord
///or here: https://www.facebook.com/Mr.D.Lord
///
///Feel free to use. Hope you'll enjoy it ^_^
///
///Here you can make fine randon rocks placing, bot spawnpoints or anything you like

using UnityEditor;
using UnityEngine;

public class MultiplyCreate : EditorWindow
{
    #region ========== Variables ========

    /// <summary>
    /// Multiplying object
    /// </summary>
    private GameObject gameObj;
    /// <summary>
    /// Quantity
    /// </summary>
    private int count;
    Vector3 startPosition;
    float spaceBetween;
    string[] directions = { "X", "-X", "Y", "-Y", "Z", "-Z" };
    int directionIndex;
    private Vector3 spawnPosition;
    private Quaternion startRotation;

    //Addiction settings
    private bool isAdvanced = false;
    private string objName = "multiplied";  //Standart name
    bool isGrouped;
    string parentsName = "Parent"; //Standart parent name

    /// <summary>
    /// Rotation of every next object
    /// </summary>
    private Quaternion nextRotation;

    private Vector3 startRot, nextRot, offset;  //temp variables

    #endregion ========== Variables ========

    [MenuItem("DeadLords/Create Multiply Objects %#d", false, 1)]
    public static void MultiplyWindow()
    {
        EditorWindow.GetWindow(typeof(MultiplyCreate));
    }

    private void OnGUI()
    {
        GUILayout.Label("Creating settings", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (Selection.activeGameObject)
        {
            gameObj = Selection.activeGameObject;   // Selecting selected in scene view object
            startPosition = gameObj.transform.position; // His position become start position
            startRot = gameObj.transform.rotation.eulerAngles;
            startRotation = gameObj.transform.rotation; // His rotation is now in start rotation
        }


        gameObj = EditorGUILayout.ObjectField("Multiplying object", gameObj, typeof(GameObject), true) as GameObject;

        count = EditorGUILayout.IntSlider("Amount of objects", count, 1, 100);

        spaceBetween = EditorGUILayout.Slider("Distance betwin objects", spaceBetween, 0, 50);

        directionIndex = EditorGUILayout.Popup("Multiply direction", directionIndex, directions);

        GUILayout.Space(5);
        startPosition = EditorGUILayout.Vector3Field("Start position of first object", startPosition);

        startRot = EditorGUILayout.Vector3Field("Rotation of the first object", startRot);
        startRotation = Quaternion.Euler(startRot);

        GUILayout.Space(5);
        nextRot = EditorGUILayout.Vector3Field("Rotation of each next object", nextRot);

        #region ===== Доп. параметры ====

        GUILayout.Space(10);
        isAdvanced = EditorGUILayout.BeginToggleGroup("Addictional settings", isAdvanced);

        objName = EditorGUILayout.TextField("Objects name", objName);

        GUILayout.Space(5);
        offset = EditorGUILayout.Vector3Field("Addictional offset", offset);

        GUILayout.Space(5);
        isGrouped = EditorGUILayout.BeginToggleGroup("Grouping under one object", isGrouped);
        parentsName = EditorGUILayout.TextField("Parent object's name", parentsName);
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.EndToggleGroup();

        #endregion ===== Доп. параметры ====

        if (GUILayout.Button("Create objects"))
        {
            if (isGrouped)
            {
                var parent = new GameObject(parentsName);
                parent.transform.position = startPosition;
                parent.transform.rotation = startRotation;

                CreateObjects(parent.transform);
            }
            else
                CreateObjects(null);
        }
    }

    /// <summary>
    /// Creating objects based on selected parameters
    /// </summary>
    /// <param name="parent">Parent name. If no need - null</param>
    private void CreateObjects(Transform parent)
    {
        Vector3 rot = new Vector3(0, 0, 0);

        switch (directionIndex)
        {
            //+x cuz default is +x
            default:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.x += spaceBetween;    // Addint distance betwin objects
                        spawnPosition += offset;    // Adding offset
                        rot += nextRot;
                        nextRotation = Quaternion.Euler(rot);   // Vector3 => Quaternion

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //-x
            case 1:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.x -= spaceBetween;
                        spawnPosition += offset;
                        rot += nextRot;
                        nextRotation = Quaternion.Euler(rot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //+y
            case 2:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.y += spaceBetween;
                        spawnPosition += offset;
                        rot += nextRot;
                        nextRotation = Quaternion.Euler(rot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //-y
            case 3:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.y -= spaceBetween;
                        spawnPosition += offset;
                        rot += nextRot;
                        nextRotation = Quaternion.Euler(rot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //+z
            case 4:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.z += spaceBetween;
                        spawnPosition += offset;
                        rot += nextRot;
                        nextRotation = Quaternion.Euler(rot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
            //-z
            case 5:
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        GameObject temp = Instantiate(gameObj, startPosition, startRotation, parent);
                        temp.name = objName;
                        spawnPosition = startPosition;
                    }
                    else
                    {
                        spawnPosition.z -= spaceBetween;
                        spawnPosition += offset;
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
        }
    }
}
