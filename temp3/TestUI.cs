using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestUI : MonoBehaviour {

    // Inside
    public Texture bloodImage;
    public Texture bloodGround;
    public float width;
    public float height;

    // Outside
    public Texture outSideImage;
    public float outWidth;
    public float outHight;

    public float displayHeight;
    public float zFactor;
    private float health = 1;

    public Transform targetTran;
    private Camera camera;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            health = Mathf.Clamp01(health - 0.1f);
        }
	}

    void OnGUI()
    {
        Vector3 tar2screen = camera.WorldToScreenPoint(targetTran.position + targetTran.up * displayHeight);
        
        if (tar2screen.x > 0 && tar2screen.x < Screen.width && tar2screen.y > 0 && tar2screen.y < Screen.height && tar2screen.z > 0)
        {
            float widthz = width / tar2screen.z * zFactor;
            float heightz = height / tar2screen.z * zFactor;
            GUI.DrawTexture(new Rect(tar2screen.x - widthz / 2, Screen.height - tar2screen.y, widthz, heightz), bloodGround);
            GUI.DrawTexture(new Rect(tar2screen.x - widthz / 2, Screen.height - tar2screen.y, widthz * health, heightz), bloodImage);
            return;
        }

        float x, y, w, h;
        x = Mathf.Clamp(tar2screen.x, 0, Screen.width);
        y = Screen.height - Mathf.Clamp(tar2screen.y, 0, Screen.height);
        w = outWidth;
        h = outHight;
        
        if (tar2screen.x >= Screen.width)
            x -= w;
        if (tar2screen.y < 0)
            y -= h;
        if (tar2screen.z < 0)
        {
            x = Screen.width - x - h;
            y = Screen.height - h;
        }
        Rect rect = new Rect(x, y, w, h);
        GUI.DrawTexture(rect, outSideImage);

        //GUILayout.Label("Clamp x : " + x);
        //GUILayout.Label("Clamp y : " + y);
        //GUILayout.Label("Tar2Screen" + tar2screen.ToString());
        //GUILayout.Label("w : " + w);
        //GUILayout.Label("h : " + h);
        //GUILayout.Label("Screen.width :" + Screen.width);
        //GUILayout.Label("Screen.height :" + Screen.height);
    }
}
