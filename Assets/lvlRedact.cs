using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class lvlRedact : MonoBehaviour
{
    public GameObject cubePref;
    public Material greenLight;
    public Material redLight;
    private GameObject previewCube;
    private Renderer previewRenderer;
    private bool canPlace=false;
    private List<GameObject> placedCubes = new List<GameObject>();
    private string saveFileName = "levelData.json";
    [System.Serializable]
    public class CubeData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    [System.Serializable]
    public class LevelData
    {
        public List<CubeData> cubes = new List<CubeData>();
    }
    void Start()
    {
        previewCube=Instantiate(cubePref);
        previewRenderer=previewCube.GetComponent<Renderer>();
        Destroy(previewCube.GetComponent<Collider>());
        Destroy(previewCube.GetComponent<Rigidbody>());
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                Vector3 objPos= new Vector3((float)Math.Round(hit.point.x), (float)Math.Round(hit.point.y)+1f, (float)Math.Round(hit.point.z));
                previewCube.transform.position=objPos;

                Collider[] overlaps = Physics.OverlapBox(previewCube.transform.position, previewCube.transform.localScale / 2.01f, Quaternion.identity);
                canPlace = true;
                foreach (var collider in overlaps)
                {
                    if (!collider.isTrigger && collider.gameObject != previewCube)
                    {
                        canPlace = false;
                        break;
                    }
                }
                previewRenderer.material=canPlace? greenLight:redLight;
            }
        
            else
            {
                canPlace=false;
                previewRenderer.material=redLight;
            }
            if (canPlace&&Input.GetMouseButtonDown(0))
            {
                GameObject newCube = Instantiate(cubePref, previewCube.transform.position, Quaternion.identity);
                placedCubes.Add(newCube);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                SaveLevel();
            }
    }
    void SaveLevel()
    {
        LevelData levelData = new LevelData();

        foreach (GameObject cube in placedCubes)
        {
            CubeData data = new CubeData
            {
                position = cube.transform.position,
                rotation = cube.transform.rotation
            };
            levelData.cubes.Add(data);
        }

        string jsonData = JsonUtility.ToJson(levelData, true);
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        
        File.WriteAllText(filePath, jsonData);
        Debug.Log($"Уровень сохранен в {filePath}");
    }

    void LoadLevel()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonData);

            foreach (GameObject cube in placedCubes)
            {
                Destroy(cube);
            }
            placedCubes.Clear();

            foreach (CubeData data in levelData.cubes)
            {
                GameObject newCube = Instantiate(cubePref, data.position, data.rotation);
                placedCubes.Add(newCube);
            }
            
            Debug.Log($"Уровень загружен из {filePath}");
        }
        else
        {
            Debug.Log("Сохраненный уровень не найден");
        }
    }
}
