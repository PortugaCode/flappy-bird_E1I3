using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    [SerializeField] private float activetime = 2f; // �ܻ� �� ������ �ð�

    [Header("�ܻ� ������ �ð�")]
    [SerializeField] private float meshRefreshRate = 0.1f; // �ܻ� ������ ����
    [SerializeField] private float destroyDelay = 2f; // ������� �ð�

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers; // �÷��̾� ���� �������� ����
    
    

    [SerializeField] private Transform spawnposition; // ���� ������ => Player



    [Header("Shader")]  // shader => Matarial �޾ƿ���
    [SerializeField] private Material mat;

    // ������ ������� �ϱ� ���� ��� ����=========================
    [SerializeField] private string shaderVarRef;
    [SerializeField] public float shadervarRate = 0.1f;
    [SerializeField] public float shaderRefreshRate = 0.05f;
    //=============================================================

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
            StartCoroutine(ActivateTrail(activetime)); // �ܻ� �ڷ�ƾ
        }
        if((player.IsArmor || player.IsRun) && !isTrailActive )
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activetime)); // �ܻ� �ڷ�ƾ
        }
    }



    private IEnumerator ActivateTrail(float timeActive)
    {
        while(timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            // ���� �ʱ�ȭ
            if (skinnedMeshRenderers == null) skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(spawnposition.position, spawnposition.rotation); // �ܻ� ������
                
                // �Ž� ���� ���� ���� ���� ���� ���� ����
                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);

                // �Ž� ����
                mf.mesh = mesh; // �Ž� ���� ���
                mr.material = mat; // material ���

                if(player.IsArmor || player.IsRun) // Run�̳� ���� ������ ���� �Ͼ�� �ܻ� �������� ����
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

    //�ܻ� ���� ���������� �ڷ�ƾ
    private IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refrehRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef); // Opacity value ���
        
        while(valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate); // ���� ���������� ����� �۾�
            yield return new WaitForSeconds(refrehRate);
        }
    }
}
