using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] TextAsset file;
    [SerializeField] GameObject player;
    [SerializeField] LineRenderer lineRend;
    private float speed = 0.001f;
    private float radius = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.points = ParseFile();
        GlobalVariables.curPoint = 0;
        GlobalVariables.nextPoint = 1;
        speed = 0.1f; // TODO test the speed
        radius = 9.144f; //change to 30 feet
        for (int i = 0; i < GlobalVariables.points.Count; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = GlobalVariables.points[i];
            sphere.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
            Renderer renderer = sphere.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.white;
            }
        }
        Vector3 pos = GlobalVariables.points[0];
        //pos.y = pos.y + 5.0f;
        player.transform.position = pos;

        if (GlobalVariables.points.Count <= 1)
        {
            GlobalVariables.errCheck = true;
            GlobalVariables.starting = false;
            GlobalVariables.isGaming = false;
            GlobalVariables.isMoving = false;
            GlobalVariables.crashed = false;
            GlobalVariables.reachCheck = false;
        }
        else
        {
            GlobalVariables.errCheck = false;
            GlobalVariables.starting = true;
            GlobalVariables.isGaming = false;
            GlobalVariables.isMoving = false;
            GlobalVariables.crashed = false;
            GlobalVariables.reachCheck = false;
            // set direction from point 0 to point 1
            GlobalVariables.flightDir = GlobalVariables.points[1] - GlobalVariables.points[0];
            GlobalVariables.flightDir = GlobalVariables.flightDir.normalized;
            player.transform.rotation = Quaternion.LookRotation(GlobalVariables.flightDir);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check error
        if (GlobalVariables.errCheck)
        {
            return;
        }

        // Run if no error
        if (GlobalVariables.starting)
        {
            // TODO (nothing to do?)
        }

        // Gaming part
        else if (GlobalVariables.isGaming)
        {
            // Position back if 
            if (GlobalVariables.crashed)
            {
                // set player back to curPoint and direction to nextPoint
                player.transform.position = GlobalVariables.points[GlobalVariables.curPoint];
                GlobalVariables.flightDir = GlobalVariables.points[GlobalVariables.nextPoint] - player.transform.position;
                GlobalVariables.flightDir = GlobalVariables.flightDir.normalized;
                player.transform.rotation = Quaternion.LookRotation(GlobalVariables.flightDir);
            }
            else
            {
                // Moving forward
                if (GlobalVariables.isMoving)
                {
                    player.transform.position += GlobalVariables.flightDir * speed;
                }

                // Rotate to the left
                if (GlobalVariables.rotateLeft)
                {
                    Quaternion left = Quaternion.Euler(0, 1, 0);
                    Matrix4x4 leftM = Matrix4x4.Rotate(left);
                    GlobalVariables.flightDir = leftM.MultiplyPoint(GlobalVariables.flightDir).normalized;
                    player.transform.rotation = Quaternion.LookRotation(GlobalVariables.flightDir);
                }

                // Rotate to the right
                if (GlobalVariables.rotateRight)
                {
                    Quaternion right = Quaternion.Euler(0, -1, 0);
                    Matrix4x4 rightM = Matrix4x4.Rotate(right);
                    GlobalVariables.flightDir = rightM.MultiplyPoint(GlobalVariables.flightDir).normalized;
                    player.transform.rotation = Quaternion.LookRotation(GlobalVariables.flightDir);
                }

                // Rotate up
                if (GlobalVariables.rotateUp)
                {
                    Quaternion up = Quaternion.Euler(-1.5f, 0, 0);
                    Matrix4x4 upM = Matrix4x4.Rotate(up);
                    GlobalVariables.flightDir = upM.MultiplyPoint(GlobalVariables.flightDir).normalized;
                    player.transform.rotation = Quaternion.LookRotation(GlobalVariables.flightDir);
                }

                // Rotate down
                if (GlobalVariables.rotateDown)
                {
                    Quaternion down = Quaternion.Euler(1.5f, 0, 0);
                    Matrix4x4 downM = Matrix4x4.Rotate(down);
                    GlobalVariables.flightDir = downM.MultiplyPoint(GlobalVariables.flightDir).normalized;
                    player.transform.rotation = Quaternion.LookRotation(GlobalVariables.flightDir);
                }
            }
            // Ray for root
            lineRend.SetVertexCount(2);
            lineRend.enabled = true;
            Vector3 shootingFrom = player.transform.position;
            shootingFrom.z = player.transform.position.z - 5.5f;
            shootingFrom.y = player.transform.position.y - 5.5f;
            lineRend.SetPosition(0, shootingFrom);
            lineRend.SetPosition(1, GlobalVariables.points[GlobalVariables.nextPoint]);
            GlobalVariables.distance = Vector3.Distance(player.transform.position, GlobalVariables.points[GlobalVariables.nextPoint]);

            // TODO for the following logic
            if (GlobalVariables.distance <= radius)
            {
                GlobalVariables.curPoint += 1;
                GlobalVariables.nextPoint += 1;
                if (GlobalVariables.nextPoint >= GlobalVariables.points.Count)
                {
                    GlobalVariables.isGaming = false;
                }
                else
                {
                    GlobalVariables.reachCheck = true;
                }
            }
        }

        // Ending
        else
        {
            GlobalVariables.distance = 0.0f;
        }
    }

    List<Vector3> ParseFile()
    {
        float ScaleFactor = 1.0f / 39.37f;
        List<Vector3> positions = new List<Vector3>();
        string content = file.ToString();
        string[] lines = content.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] coords = lines[i].Split(' ');
            Vector3 pos = new Vector3(float.Parse(coords[0]), float.Parse(coords[1]), float.Parse(coords[2]));
            positions.Add(pos * ScaleFactor);
        }
        return positions;
    }

}
