using NavMeshPlus.Extensions;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting.Dependencies.Sqlite;
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
        private float MovementThreshold = 3;
        [SerializeField]
        public Vector3 NavMeshSize = new Vector3(20f, 20f, 20f);


        private Vector3 WorldAnchor;
        private NavMeshData NavMeshData;
        private List<NavMeshBuildSource> Sources = new List<NavMeshBuildSource>();

        void Start()
        {
            NavMeshData = new NavMeshData();
            NavMesh.AddNavMeshData(NavMeshData);
            BuildNavMesh(false);
            /*            StartCoroutine(CheckPlayerMovement());*/
        }

        private void Update()
        {
            CheckPlayerMovement();
            BuildNavMesh(true);
        }

        private void CheckPlayerMovement()
        {
            /*WaitForSeconds wait = new WaitForSeconds(updateRate);

            while (true)
            {
                if (Vector3.Distance(WorldAnchor, player.transform.position) > MovementThreshold)
                {
                    BuildNavMesh(true);
                    WorldAnchor = player.transform.position;
                }
                yield return wait;
            }*/
            if (player != null) // ??m b?o target t?n t?i
            {
                BuildNavMesh(true);
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            }
        }

        private void BuildNavMesh(bool Async)
        {
            Bounds navMeshBounds = new Bounds(player.transform.position, NavMeshSize);  // Xác ??nh khu v?c NavMesh d?a trên v? trí player
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

            // Xoá các ngu?n có thành ph?n NavMeshAgent
            Sources.RemoveAll(source => source.component != null && source.component.gameObject.GetComponent<NavMeshAgent>() != null);

            // C?p nh?t NavMesh
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




