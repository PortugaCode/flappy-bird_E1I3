using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    [SerializeField] private float activetime = 2f;

    [Header("잔상 나오는 시간")]
    [SerializeField] private float meshRefreshRate = 0.1f;
    [SerializeField] private float destroyDelay = 2f;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    
    

    [SerializeField] private Transform spawnposition;

    [Header("Shader")]
    [SerializeField] private Material mat;

    [SerializeField] private string shaderVarRef;
    [SerializeField] public float shadervarRate = 0.1f;
    [SerializeField] public float shaderRefreshRate = 0.05f;

    private PlayerController player;

    private void Awake()
    {
        TryGetComponent(out player);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && !isTrailActive && !player.IsArmor)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activetime));
        }
        if(player.IsArmor && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activetime));
        }
    }

    private IEnumerator ActivateTrail(float timeActive)
    {
        while(timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderers == null) skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(spawnposition.position, spawnposition.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                if(player.IsArmor)
                {
                    mr.material.SetColor("Color_3e9d31d8169b419ea8cb0a26c5e71f67", new Color (39f,42f,191f));
                    mr.material.SetFloat(shaderVarRef, 0.5f);
                }

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, shadervarRate, shaderRefreshRate));

                Destroy(gObj, destroyDelay);

            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

    private IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refrehRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef);
        
        while(valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refrehRate);
        }
    }
}
