///Made by @MrDeadLord
///Here you can make fine randon rocks placing, bot spawnpoints or anything you like
///Apply any suggestions of making it shorter or better ^_^
///Hope it'll help anyone

using UnityEditor;
using UnityEngine;

public class MultiplyCreate : EditorWindow
{
    #region Variables
    /// <summary>
    /// Multiplying object
    /// </summary>
    GameObject gameObj;
    /// <summary>
    /// Amount of multiply objects
    /// </summary>
    int count;
    Vector3 startPosition;
    float spaceBetween;
    string[] directions = { "X", "-X", "Y", "-Y", "Z", "-Z" };
    int directionIndex;
    Vector3 spawnPosition;
    Quaternion startRotation;

    //Random's
    bool randStartX, randStartY, randStartZ;
    bool randNextX, randNextY, randNextZ;

    //Addictional param's
    bool isAdvanced = false;
    string objName = "multiplied";  //Standart name
    bool isGrouped;
    string parentsName = "Parrent"; //standart parrent name
    
    /// <summary>
    /// Angle of each next object
    /// </summary>
    Quaternion nextRotation;

    Vector3 startRot, nextRot;  //temporary variables
    #endregion

    [MenuItem("DeadLords/Create Multiply Objects %#d", false, 1)]
    public static void MultiplyWindow()
    {
        EditorWindow.GetWindow(typeof(MultiplyCreate));
    }

    private void OnGUI()
    {
        GUILayout.Label("Multiply settings", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (Selection.activeGameObject)
        {
            gameObj = Selection.activeGameObject;   //If any object selected - it goes here
            startPosition = gameObj.transform.position; //it's start position writes here

            startRot = gameObj.transform.rotation.eulerAngles;
            startRotation = gameObj.transform.rotation; //Start angle of a selected object
        }


        gameObj = EditorGUILayout.ObjectField("Multiply object", gameObj, typeof(GameObject), true) as GameObject;

        count = EditorGUILayout.IntSlider("Amount of objects", count, 1, 100);

        spaceBetween = EditorGUILayout.Slider("Distance betwin objects", spaceBetween, 1, 50);

        directionIndex = EditorGUILayout.Popup("Direction", directionIndex, directions);

        GUILayout.Space(5);
        startPosition = EditorGUILayout.Vector3Field("Start position(first obj. pos.)", startPosition);

        startRot = EditorGUILayout.Vector3Field("Angle of the first object", startRot);
                        
        //Randomizing parameters
        GUILayout.BeginHorizontal();
        randStartX = GUILayout.Toggle(randStartX, "Random X");
        randStartY = GUILayout.Toggle(randStartY, "Random Y");
        randStartZ = GUILayout.Toggle(randStartZ, "Random Z");
        GUILayout.EndHorizontal();

        Rotate(true);

        GUILayout.Space(5);
        nextRot = EditorGUILayout.Vector3Field("Angle of next objects", nextRot);

        //Randomizing parameters
        GUILayout.BeginHorizontal();
        randNextX = GUILayout.Toggle(randNextX, "Random X");
        randNextY = GUILayout.Toggle(randNextY, "Random Y");
        randNextZ = GUILayout.Toggle(randNextZ, "Random Z");
        GUILayout.EndHorizontal();

        #region Additional params
        GUILayout.Space(10);
        isAdvanced = EditorGUILayout.BeginToggleGroup("Additional params", isAdvanced);

        objName = EditorGUILayout.TextField("Objects name", objName);

        GUILayout.Space(5);
        isGrouped = EditorGUILayout.BeginToggleGroup("Group under one object(empty)", isGrouped);
        parentsName = EditorGUILayout.TextField("Name of parrent object", parentsName);
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.EndToggleGroup();
        #endregion

        if (GUILayout.Button("Create objects"))
        {
            if (isGrouped)
            {
                var parent = new GameObject(parentsName);
                parent.transform.position = startPosition;
                parent.transform.rotation = startRotation;

                CreateObject(parent.transform);
            }
            else
                CreateObject(null);
        }
    }

    /// <summary>
    /// Creating objects
    /// </summary>
    /// <param name="parent">Parent object. If no - null</param>
    private void CreateObject(Transform parent)
    {
        switch (directionIndex)
        {
            //+x. Default is +x
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
                        spawnPosition.x += spaceBetween;

                        Rotate(false);
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

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

                        Rotate(false);
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

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

                        Rotate(false);
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

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

                        Rotate(false);
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

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

                        Rotate(false);
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

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

                        Rotate(false);
                        nextRot *= i;
                        nextRotation = Quaternion.Euler(nextRot);

                        GameObject temp = Instantiate(gameObj, spawnPosition, nextRotation, parent);
                        temp.name = objName + "(" + i + ")";
                    }
                }
                break;
        }        
    }

    /// <summary>
    /// Randomize rotation if needed
    /// </summary>
    /// <param name="isStart">Is it start rotation angles or not</param>
    void Rotate(bool isStart)
    {        
        if (isStart)
        {
            if (randStartX)
                startRot.x = Random.Range(0, 359);
            if (randStartY)
                startRot.y = Random.Range(0, 359);
            if (randStartZ)
                startRot.z = Random.Range(0, 359);

            startRotation = Quaternion.Euler(startRot);
        }
        else
        {
            if (randNextX)
                nextRot.x = Random.Range(0, 359);
            if (randNextY)
                nextRot.y = Random.Range(0, 359);
            if (randNextZ)
                nextRot.z = Random.Range(0, 359);
        }
    }
}