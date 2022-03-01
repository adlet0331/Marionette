using System;
using System.Collections.Generic;

[Serializable]
public struct LockData
{
    public int idx;
    public string interactName;
    public string interactString;
    public List<LockInfo> lockInfoList;
};

public class LockDataManager : Singleton<LockDataManager>
{
    public List<LockData> LockDataList;
}
