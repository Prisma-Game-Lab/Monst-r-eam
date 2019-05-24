using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script bobão pra eu chamar uma funcção estática a partir do menu de configurações
public class ClearSaveHelper : MonoBehaviour
{
   public void ClearSave()
   {
       SaveSystem.DeleteSaveFile();
   }
}
