using NavMeshPlus.Extensions;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using NavMeshBuider = UnityEngine.AI.NavMeshBuilder;

namespace NavMeshPlus.Components
{
    public class AreaMapBaker : MonoBehaviour
    {
        [SerializeField]
        public NavMeshSurface surface;
        [SerializeField]
        public GameObject player;
        [SerializeField]
        public float updateRate = 0.1f;
        [SerializeField]
        public Vector3 NavMeshSize = new Vector3(20f, 20f, 0f); // ?i?u ch?nh kích th??c cho 2D (chi?u z = 0)

        private NavMeshData NavMeshData;
        private List<NavMeshBuildSource> Sources = new List<NavMeshBuildSource>();

        void Start()
        {
            NavMeshData = new NavMeshData();
            NavMesh.AddNavMeshData(NavMeshData);
            BuildNavMesh(false);
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
        }

        private void Update()
        {
            CheckPlayerMovement();
            BuildNavMesh(true);
        }

        private void CheckPlayerMovement()
        {
            if (player != null)
            {
                BuildNavMesh(true);
                // ?i?u ch?nh t?a ?? z b?ng 0 cho 2D
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
            }
        }

        private void BuildNavMesh(bool Async)
        {
            // Ch?nh Bounds ch? theo XY, lo?i b? z vì là 2D
            Bounds navMeshBounds = new Bounds(new Vector3(player.transform.position.x, player.transform.position.y, 0), NavMeshSize);
            List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();
            List<NavMeshModifier> modifiers;

            if (surface.collectObjects == CollectObjects.All)
            {
                modifiers = new List<NavMeshModifier>(surface.GetComponentsInChildren<NavMeshModifier>());
            }
            else
            {
                modifiers = NavMeshModifier.activeModifiers;
            }

            // X? lý markups cho các modifier ?nh h??ng ??n NavMesh
            for (int i = 0; i < modifiers.Count; i++)
            {
                if (((surface.layerMask & (1 << modifiers[i].gameObject.layer)) == 1)
                && modifiers[i].AffectsAgentType(surface.agentTypeID))
                {
                    markups.Add(new NavMeshBuildMarkup()
                    {
                        root = modifiers[i].transform,
                        overrideArea = modifiers[i].overrideArea,
                        area = modifiers[i].area,
                        ignoreFromBuild = modifiers[i].ignoreFromBuild
                    });
                }
            }

            // Thu th?p ngu?n NavMesh
            if (surface.collectObjects == CollectObjects.All)
            {
                NavMeshBuilder.CollectSources(surface.transform, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, Sources);
            }
            else
            {
                NavMeshBuilder.CollectSources(navMeshBounds, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, Sources);
            }

            // Xóa các ngu?n có thành ph?n NavMeshAgent
            Sources.RemoveAll(source => source.component != null && source.component.gameObject.GetComponent<NavMeshAgent>() != null);

            // C?p nh?t NavMesh cho 2D
            if (Async)
            {
                NavMeshBuilder.UpdateNavMeshDataAsync(NavMeshData, surface.GetBuildSettings(), Sources, navMeshBounds);
            }
            else
            {
                NavMeshBuilder.UpdateNavMeshData(NavMeshData, surface.GetBuildSettings(), Sources, navMeshBounds);
            }
        }
    }
}
