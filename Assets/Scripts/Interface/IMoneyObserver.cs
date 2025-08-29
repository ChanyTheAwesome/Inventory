using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoneyObserver
{
    void OnMoneyChanged(int money);
}
