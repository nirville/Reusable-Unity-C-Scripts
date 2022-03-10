using UnityEngine;
using System.Collections;
 
public class BuildingPlacement : MonoBehaviour {
       
        public float scrollSensitivity;
       
        private PlaceableBuilding placeableBuilding;
        private Transform currentBuilding;
        private bool hasPlaced;
        public Transform transform;
       
        public LayerMask buildingsMask;
        //public Vector3 point;
        public PlaceableBuilding placeableBuildingOld;
        public float  RotateSelf = 3f;
       
 
       
        // Update is called once per frame
        void Update () {
               
               
 
                if (currentBuilding != null && !hasPlaced) {
                       
                        RaycastHit hit = new RaycastHit();
                        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                        Physics.Raycast(ray,out hit, Mathf.Infinity);
               
                        hit.point = new Vector3(hit.point.x, 0.5f, hit.point.z);
                        currentBuilding.transform.position =  hit.point;
                       
                        if (Input.GetButtonDown("r") || (Input.GetButton("r"))){
                               
                                currentBuilding.transform.Rotate(Vector3.up, RotateSelf, Space.Self);
                                }
                               
                        if (Input.GetButtonDown("q") || (Input.GetButton("q"))){
                               
                                currentBuilding.transform.Rotate(Vector3.down, RotateSelf, Space.Self);
                                }
                               
                               
                        if(Input.GetButtonDown("Cancel"))
                        {
                               
                                Destroy(currentBuilding.gameObject);
                               
                        }
                       
                       
                       
                        if (Input.GetMouseButtonDown(0)) {
                                if (IsLegalPosition()) {
                                        hasPlaced = true;      
                                }
                        }
                }
                else {
                        if (Input.GetMouseButtonDown(0)) {
                                RaycastHit hit = new RaycastHit();
                                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                                Physics.Raycast(ray,out hit, Mathf.Infinity, buildingsMask);
                               
                               
                                if (Physics.Raycast(ray, out hit,Mathf.Infinity)) {
                                        if (placeableBuildingOld != null) {
                                                placeableBuildingOld.SetSelected(false);
                                        }
                                        hit.collider.gameObject.GetComponent<PlaceableBuilding>().SetSelected(true);
                                        placeableBuildingOld = hit.collider.gameObject.GetComponent<PlaceableBuilding>();
                                }
                                else {
                                        if (placeableBuildingOld != null) {
                                                placeableBuildingOld.SetSelected(false);
                                        }
                                }
                        }
                }
        }
 
 
        bool IsLegalPosition() {
                if (placeableBuilding.colliders.Count > 0) {
                        return false;  
                }
                return true;
        }
       
        public void SetItem(GameObject b) {
                hasPlaced = false;
                currentBuilding = ((GameObject)Instantiate(b)).transform;
                placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
        }
}