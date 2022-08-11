using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffCreditUIManager : MonoBehaviour
{
    [SerializeField] private GameObject licensesPanelGameObject;
    
    public void OnClickStaffCreditExitButton()
    {
        gameObject.SetActive(false);
    }

    public void OnClickLicensesButton()
    {
        licensesPanelGameObject.SetActive(true);
    }
    
    public void OnClickLicensesPanelExitButton()
    {
        licensesPanelGameObject.SetActive(false);
    }
}
