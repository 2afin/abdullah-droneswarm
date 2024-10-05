public class Flock
{
    Drone[] agents;
    int num;
    
    public Flock(int maxnum)
    {
        agents = new Drone[maxnum];
    }
    
    // actually add the drones
    public void Init(int num)
    {
        this.num = num;
        for (int i=0; i<num; i++)
        {
            agents[i] = new Drone(i);
        }
    }
    
    public void Update()
    {
        for (int i=0; i<num; i++)
        {
            agents[i].Update();
        }
    }
    
    public float average()
    //MUHAMMAD FAIQ HAKEEM BIN FARID 24000054
    {
        float sum = 0;
        for (int i = 0; i < num; i++)
        {
            sum += agents[i].Temperature;
        }
        return sum / num;
    }

    public float max()
    //DZURIYAT ILHAN BIN MOHD RIDZUAN 24000061
    {
        float maxValue = agents[0].Temperature;
        for (int i = 1; i < num; i++)
        {
            if (agents[i].Temperature > maxValue)
            {
                maxValue = agents[i].Temperature;
            }
        }
        return maxValue;
    }

    public float min()
    //AHMAD AQIL FAHMI BIN AHMAD NOR 24000235
    {
        float minValue = agents[0].Temperature;
        for (int i = 1; i < num; i++)
        {
            if (agents[i].Temperature < minValue)
            {
                minValue = agents[i].Temperature;
            }
        }
        return minValue;
    }

    public void print()
    {
    }

    public void append(Drone val)
    {
    }

    public void appendFront(Drone val)
    {
    }


    public void insert(Drone val, int index)
    {

    }

    public void DeleteFront()
    //DANISH SAFIN BIN ZULKARNAIN 24000149
    {
        if (num == 0)
        {
            return;
        }
        for (int i = 0; i < num - 1; i++) //shift everything to the lift
        {
            agents[i] = agents[i + 1];
        }
        num--; //ignore last element
    }


    public void deleteBack(int index)
    {

    }


    public void delete(int index)
    {

    } 
    
    public void bubblesort()
    //ABDULLAH SHAHIR BIN ZULMAJDI 24000112
    {
        int i, j;
        Drone temp;
        bool swapped;

        for (i = 0; i < num - 1; i++)  // Outer loop to control passes
        {
            swapped = false;
            for (j = 0; j < num - i - 1; j++)  // Inner loop for comparisons
            {
                // Sort by ID; change this to Temperature, Wind, or Battery if needed
                if (agents[j].ID > agents[j + 1].ID)
                {
                    // Swap agents[j] and agents[j+1]
                    temp = agents[j];
                    agents[j] = agents[j + 1];
                    agents[j + 1] = temp;
                    swapped = true;
                }
            }
            // Early exit optimization: Break if no swaps occurred
            if (!swapped)
                break;
        }
    }
    public void insertionsort()
    {
        
    }

}