using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CVumarkHandler : MonoBehaviour {

    #region CONSTANTS
    public const string FirstTable = "0000000001";
    public const string SecondTable = "0000000002";
    #endregion // CONSTANTS

    #region PRIVATE_MEMBER_VARIABLES
    [SerializeField, Header ("Mostradores")]
    List<GameObject> _tables;
    [SerializeField]
    GameObject _tablePool;
    private VuMarkManager mVuMarkManager;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNTIY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        // register callbacks to VuMark Manager
        mVuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
        mVuMarkManager.RegisterVuMarkBehaviourDetectedCallback(OnVumarkBehaviourDetected);
        mVuMarkManager.RegisterVuMarkDetectedCallback(OnVuMarkDetected);
        mVuMarkManager.RegisterVuMarkLostCallback(OnVuMarkLost);
    }

    void OnDestroy()
    {
        // unregister callbacks from VuMark Manager
        mVuMarkManager.UnregisterVuMarkDetectedCallback(OnVuMarkDetected);
        mVuMarkManager.UnregisterVuMarkLostCallback(OnVuMarkLost);
    }

    void Update()
    {
        UpdateTables();
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    
    public void OnVumarkBehaviourDetected(VuMarkAbstractBehaviour pBehaviour)
    {

    }

    /// This method will be called whenever a new VuMark is detected
    public void OnVuMarkDetected(VuMarkTarget pTarget)
    {

    }

    /// This method will be called whenever a tracked VuMark is lost
    public void OnVuMarkLost(VuMarkTarget pTarget)
    {
        VuMarkTarget tVumarkTarget;
        foreach (var iVumark in mVuMarkManager.GetActiveBehaviours())
        {
            tVumarkTarget = iVumark.VuMarkTarget;
            if (GetVuMarkString(tVumarkTarget) == FirstTable)
            {
                _tables[0].transform.parent = _tablePool.transform;
                _tablePool.transform.localPosition = new Vector3(0,0,0);
                _tables[0].SetActive(false);
            }
            else if (GetVuMarkString(tVumarkTarget) == SecondTable)
            {
                _tables[1].transform.parent = _tablePool.transform;
                _tablePool.transform.localPosition = new Vector3(0, 0, 0);
                _tables[1].SetActive(false);
            }
        }
    }
    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    // Return vumark ID
    private string GetVuMarkString(VuMarkTarget pVumark)
    {
        switch (pVumark.InstanceId.DataType)
        {
            case InstanceIdType.BYTES:
                return pVumark.InstanceId.HexStringValue;
            case InstanceIdType.STRING:
                return pVumark.InstanceId.StringValue;
            case InstanceIdType.NUMERIC:
                return pVumark.InstanceId.NumericValue.ToString();
        }
        return "";
    }

    public void UpdateTables()
    {
        VuMarkTarget tVumarkTarget;
        foreach (var iVumark in mVuMarkManager.GetActiveBehaviours()) // The gameobject of iVumark is "VuMark" Instance on hierarchy. 
        {
            tVumarkTarget = iVumark.VuMarkTarget;
            if (GetVuMarkString(tVumarkTarget) == FirstTable)
            {
                _tables[0].transform.parent = iVumark.transform;
                _tables[0].transform.localPosition = new Vector3(0.45f, -0.4f, 0);
                _tables[0].SetActive(true);
            }
            else if (GetVuMarkString(tVumarkTarget) == SecondTable)
            {
                _tables[1].transform.parent = iVumark.transform;
                _tables[1].transform.localPosition = new Vector3(0.45f, -0.4f, 0);
                _tables[1].SetActive(true);
            }
        }
    }

    #endregion // PRIVATE_METHODS
}
