using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeManager : MonoBehaviour
{
    #region Unity Declarations

        public static CostumeManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        [Header("Player Model Section")]
        
        private int player_costume_index;

        [SerializeField] private GameObject PlayerModel;

        [SerializeField] private GameObject MalePlayer;

        [SerializeField] private GameObject FemalePlayer;

        [Header("Player Male Section")]

        [SerializeField] private GameObject MalePlayerModelHead;

        [SerializeField] private GameObject MalePlayerModelHair;

        [SerializeField] private GameObject MalePlayerModelTorso;

        [SerializeField] private GameObject MalePlayerModelLeg;

        [SerializeField] private Mesh[] PlayerMaleHeadMesh;

        [SerializeField] private Mesh[] PlayerMaleHairMesh;

        [SerializeField] private Mesh[] PlayerMaleTorsoMesh;

        [SerializeField] private Mesh[] PlayerMaleLegMesh;

        [SerializeField] private Avatar MaleAnimAvatar;

        [Header("Player Female Section")]

        [SerializeField] private GameObject FemalePlayerModelHead;

        [SerializeField] private GameObject FemalePlayerModelHair;

        [SerializeField] private GameObject FemalePlayerModelTorso;

        [SerializeField] private GameObject FemalePlayerModelLeg;

        [SerializeField] private Mesh[] PlayerFemaleHeadMesh;

        [SerializeField] private Mesh[] PlayerFemaleHairMesh;

        [SerializeField] private Mesh[] PlayerFemaleTorsoMesh;

        [SerializeField] private Mesh[] PlayerFemaleLegMesh;

        [SerializeField] private Avatar FemaleAnimAvatar;

    #endregion

    #region Private Methods

        public void SetActivePlayerModel(bool index)
        {
            PlayerModel.SetActive(index);
        }

        public void SetActiveMalePlayer(bool index)
        {
            MalePlayer.SetActive(index);
        }

        public void SetActiveFemalePlayer(bool index)
        {
            FemalePlayer.SetActive(index);
        }

        public void TransformPlayerTo(Transform index)
        {
            PlayerModel.transform.position = index.position; 

            PlayerModel.transform.rotation = index.rotation; 
        }

        public void ChangeCostumeTo(int costumeIndex, string genderIndex)
        {
            switch(genderIndex)
            {
                case "BOY":

                    SetActiveMalePlayer(true);

                    PlayerModel.GetComponent<Animator>().avatar = MaleAnimAvatar;

                    MalePlayerModelHead.GetComponent<SkinnedMeshRenderer>().sharedMesh = PlayerMaleHeadMesh[costumeIndex];

                    MalePlayerModelHair.GetComponent<MeshFilter>().mesh = PlayerMaleHairMesh[costumeIndex];

                    MalePlayerModelTorso.GetComponent<SkinnedMeshRenderer>().sharedMesh = PlayerMaleTorsoMesh[costumeIndex];

                    MalePlayerModelLeg.GetComponent<SkinnedMeshRenderer>().sharedMesh = PlayerMaleLegMesh[costumeIndex];

                    SetActiveFemalePlayer(false);

                    break;

                case "GIRL":

                    SetActiveFemalePlayer(true);

                    PlayerModel.GetComponent<Animator>().avatar = FemaleAnimAvatar;

                    FemalePlayerModelHead.GetComponent<SkinnedMeshRenderer>().sharedMesh = PlayerFemaleHeadMesh[costumeIndex];

                    FemalePlayerModelHair.GetComponent<MeshFilter>().mesh = PlayerFemaleHairMesh[costumeIndex];

                    FemalePlayerModelTorso.GetComponent<SkinnedMeshRenderer>().sharedMesh = PlayerFemaleTorsoMesh[costumeIndex];

                    FemalePlayerModelLeg.GetComponent<SkinnedMeshRenderer>().sharedMesh = PlayerFemaleLegMesh[costumeIndex];

                    SetActiveMalePlayer(false);

                    break;
            }
        }

        public void AdjustCostumeIndex(int index)
        {
            player_costume_index += index;

            if(player_costume_index < 0)
            {
                player_costume_index = PlayerMaleHeadMesh.Length - 1;
            }
            else if(player_costume_index > PlayerMaleHeadMesh.Length - 1)
            {
                player_costume_index = 0;
            }

            ChangeCostumeTo(player_costume_index, AppUIManager.Instance.SendStringGenderInput());

            AppUIManager.Instance.ReadIntCostumeInput(player_costume_index);
        }

        public void ResetGenderCostume(string index)
        {
            ChangeCostumeTo(0,index);

            player_costume_index = 0;
        }

        

    #endregion
}
