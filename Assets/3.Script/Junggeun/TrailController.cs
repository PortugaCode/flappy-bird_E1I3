using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    [SerializeField] private float activetime = 2f; // 잔상 총 나오는 시간

    [Header("잔상 나오는 시간")]
    [SerializeField] private float meshRefreshRate = 0.1f; // 잔상 나오는 간격
    [SerializeField] private float destroyDelay = 2f; // 사라지는 시간

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers; // 플레이어 렌더 가져오는 역할
    
    

    [SerializeField] private Transform spawnposition; // 스폰 포지션 => Player



    [Header("Shader")]  // shader => Matarial 받아오기
    [SerializeField] private Material mat;

    // 서서히 사라지게 하기 위한 멤버 변수=========================
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
            StartCoroutine(ActivateTrail(activetime)); // 잔상 코루틴
        }
        if((player.IsArmor || player.IsRun) && !isTrailActive )
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activetime)); // 잔상 코루틴
        }
    }



    private IEnumerator ActivateTrail(float timeActive)
    {
        while(timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            // 렌더 초기화
            if (skinnedMeshRenderers == null) skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(spawnposition.position, spawnposition.rotation); // 잔상 포지션
                
                // 매쉬 렌더 관련 정보 담을 지역 변수 선언
                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);

                // 매쉬 설정
                mf.mesh = mesh; // 매쉬 필터 담기
                mr.material = mat; // material 담기

                if(player.IsArmor || player.IsRun) // Run이나 무적 상태일 때는 하얀색 잔상 나오도록 설정
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

    //잔상 점점 투명해지는 코루틴
    private IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refrehRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef); // Opacity value 담기
        
        while(valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate); // 점점 투명해지게 만드는 작업
            yield return new WaitForSeconds(refrehRate);
        }
    }
}
