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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isTrailActive)
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

                Destroy(gObj, destroyDelay);

            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }
}
