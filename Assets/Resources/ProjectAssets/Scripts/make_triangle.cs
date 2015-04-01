using UnityEngine;
using System.Collections;

public class make_triangle : MonoBehaviour {

	private int VERTICES_COUNT = 6;  
	
	void Start()  
	{  
		MeshFilter meshFilter = (MeshFilter)GameObject.Find("MeshMat").GetComponent(typeof(MeshFilter));  
		
		Mesh mesh = meshFilter.mesh;  
		
		Vector3[] vertices = new Vector3[VERTICES_COUNT];  
		vertices[0] = new Vector3(0,0,0);  
		vertices[1] = new Vector3(0,1,0);  
		vertices[2] = new Vector3(1,0,0);  
		
		vertices[3] = new Vector3(1,1,0);  
		vertices[4] = new Vector3(2,0,0);  
		vertices[5] = new Vector3(2,1,0);  
		
		int triangles_count = VERTICES_COUNT - 2;  
		int[] triangles = new int[triangles_count * 3];  
		
		for(int i =0;i < 4; i++)  
		{  
			for(int j =0;j < 3;j++)  
			{  
				if(i % 2 ==0)  
				{  
					triangles[i* 3 +j] = i+j;  
				}  
				else  
				{  
					triangles[i * 3 +j] = i + 2 -j;  
				}  
				
			}  
		}  
		mesh.vertices = vertices;  
		mesh.triangles = triangles;  
		
		
	}  
}

