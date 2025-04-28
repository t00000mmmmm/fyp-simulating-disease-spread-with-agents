using System;
using UnityEngine;

[Serializable]
public struct serial
{
    // Start is called before the first frame update
    public int sneezing;
    public int coughing;
    public int pop;

    public serial(int sneezing, int coughing,int pop)
    {
        this.sneezing = sneezing;
        this.coughing = coughing;
        this.pop = pop;
    }

    

    // Update is called once per frame
    void Update()
    {

    }
}
