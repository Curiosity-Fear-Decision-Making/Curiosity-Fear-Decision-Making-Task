using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class script_draw_sector : MonoBehaviour
{
    public Material cur_material;
    public float ref_angle = 0f;
    public int idx_sector_color = 0;
    public float outerR_ratio = 2f;

    void Start()
    {
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        // 准备组件
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        MeshCollider mc = GetComponent<MeshCollider>();
        if (mc == null) mc = gameObject.AddComponent<MeshCollider>();
        mc.convex = true;

        // 材质
        mr.material = new Material(cur_material);
        mr.material.SetColor("_Color", script_main.fear_color_list_in_game[idx_sector_color]);
        mr.material.SetFloat("_Cull", 0);
        mr.material.SetFloat("_Metallic", 0f);
        mr.material.SetFloat("_Glossiness", 0f);

        // 参数
        float thickness = 0.5f;
        int sectorAngle = script_main.sector_angle;
        float outerR = script_main.ground_size / outerR_ratio;
        float innerR = Mathf.Clamp(outerR * 0.5f, 0.001f, outerR - 0.001f);

        float startDeg = ref_angle - sectorAngle / 2f;
        float endDeg   = ref_angle + sectorAngle / 2f;

        int seg = Mathf.Max(1, sectorAngle);
        int vPerRing = seg + 1;
        Vector3[] v = new Vector3[vPerRing * 4];
        float yTop =  thickness / 2f;
        float yBot = -thickness / 2f;
        float step = (endDeg - startDeg) / seg;

        int idx = 0;
        for (int i = 0; i <= seg; i++) { float a = Mathf.Deg2Rad * (startDeg + step*i); v[idx++] = new Vector3(Mathf.Cos(a)*outerR, yTop, Mathf.Sin(a)*outerR); }
        for (int i = 0; i <= seg; i++) { float a = Mathf.Deg2Rad * (startDeg + step*i); v[idx++] = new Vector3(Mathf.Cos(a)*innerR, yTop, Mathf.Sin(a)*innerR); }
        for (int i = 0; i <= seg; i++) { float a = Mathf.Deg2Rad * (startDeg + step*i); v[idx++] = new Vector3(Mathf.Cos(a)*outerR, yBot, Mathf.Sin(a)*outerR); }
        for (int i = 0; i <= seg; i++) { float a = Mathf.Deg2Rad * (startDeg + step*i); v[idx++] = new Vector3(Mathf.Cos(a)*innerR, yBot, Mathf.Sin(a)*innerR); }

        List<int> t = new List<int>(seg * 24 + 12);
        int iTopOuter = 0, iTopInner = vPerRing, iBotOuter = vPerRing * 2, iBotInner = vPerRing * 3;

        for (int i = 0; i < seg; i++) { int a=iTopOuter+i,b=a+1,c=iTopInner+i,d=c+1; t.Add(a);t.Add(c);t.Add(b); t.Add(b);t.Add(c);t.Add(d); }
        for (int i = 0; i < seg; i++) { int a=iBotOuter+i,b=a+1,c=iBotInner+i,d=c+1; t.Add(a);t.Add(b);t.Add(c); t.Add(b);t.Add(d);t.Add(c); }
        for (int i = 0; i < seg; i++) { int tA=iTopOuter+i,tB=tA+1,bA=iBotOuter+i,bB=bA+1; t.Add(tA);t.Add(bA);t.Add(bB); t.Add(tA);t.Add(bB);t.Add(tB); }
        for (int i = 0; i < seg; i++) { int tA=iTopInner+i,tB=tA+1,bA=iBotInner+i,bB=bA+1; t.Add(tA);t.Add(bB);t.Add(bA); t.Add(tA);t.Add(tB);t.Add(bB); }

        // 起始与结束角侧壁
        { int tO=iTopOuter,tI=iTopInner,bO=iBotOuter,bI=iBotInner; t.Add(tO);t.Add(bO);t.Add(bI); t.Add(tO);t.Add(bI);t.Add(tI); }
        { int tO=iTopOuter+seg,tI=iTopInner+seg,bO=iBotOuter+seg,bI=iBotInner+seg; t.Add(tO);t.Add(bI);t.Add(bO); t.Add(tO);t.Add(tI);t.Add(bI); }

        Mesh mesh = new Mesh();
        mesh.name = "SectorRing";
        mesh.SetVertices(v);
        mesh.SetTriangles(t, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        mf.sharedMesh = mesh;
        mc.sharedMesh = mesh;
    }
}