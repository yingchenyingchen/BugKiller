using UnityEngine;
using System.Collections;

public class roof_triangle : MonoBehaviour {

	// Use this for initialization  
	void Start ()   
	{  
		//获取MeshFilter对象  现在还是空的。  
		MeshFilter meshFilter = (MeshFilter)GameObject.Find("MeshMat").GetComponent(typeof(MeshFilter));  
		//得到对应的网格对象  
		Mesh mesh = meshFilter.mesh;  
		//三角形顶点坐标数组  
		Vector3[] vertices = new Vector3[3];  
		//三角形的坐标点  ，这里先不考虑Z轴  
		vertices[0] = new Vector3(-10,0,-20);  
		vertices[1] = new Vector3(0,0,0);  
		vertices[2] = new Vector3(0,0,-20);  
		//三角形顶点数组ID  
		int[] triangles = new int[3];  
		
		triangles[0] = 0;  
		triangles[1] = 1;  
		triangles[2] = 2;  
		
		//这里是将模型的顶点数组与坐标数组赋值给网络模型，还记得刚刚在  
		//创建MeshFilter时，当时没有在编辑器里为网络模型赋值，实际上代码走到这里就会重新为  
		//  网络模型创建MeshFilter赋值的，接着我们再代码中绘制的三角形就会显示的窗口中  
		mesh.vertices = vertices;  
		mesh.triangles = triangles;  
	}  
	
	
	// Update is called once per frame  
	void Update () {  
		
	}  
}


