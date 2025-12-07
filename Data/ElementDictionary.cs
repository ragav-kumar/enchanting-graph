namespace EnchantingGraph.Data;

public class ElementDictionary : Dictionary<Element, float>
{
    public ElementDictionary()
    {
    }

    public ElementDictionary(ElementDictionary dict) : base(dict)
    {
    }

    public ElementDictionary(int capacity) : base(capacity)
    {
    }

    public new float this[Element index]
    {
        get
        {
            if (!TryGetValue(index, out float result))
            {
                result = 0f;
            }

            return result;
        }
        set => base[index] = value;
    }

    public static ElementDictionary operator +(ElementDictionary dict, float scalar)
    {
        ArgumentNullException.ThrowIfNull(dict);

        ElementDictionary result = new(dict);
        foreach (KeyValuePair<Element, float> pair in result)
        {
            result[pair.Key] = pair.Value + scalar;
        }
        
        return result;
    }

    public static ElementDictionary operator +(ElementDictionary dict, ElementDictionary dict2)
    {
        ArgumentNullException.ThrowIfNull(dict);
        ArgumentNullException.ThrowIfNull(dict2);
        
        ElementDictionary result = new(dict);
        foreach (KeyValuePair<Element, float> pair in dict2)
        {
            result[pair.Key] += pair.Value;
        }
        return result;
    }

    public static ElementDictionary operator *(ElementDictionary dict, float scalar)
    {
        ArgumentNullException.ThrowIfNull(dict);

        ElementDictionary result = new(dict);
        foreach (KeyValuePair<Element, float> pair in result)
        {
            result[pair.Key] = pair.Value *  scalar;
        }
        
        return result;
    }

    public static ElementDictionary operator *(ElementDictionary dict, ElementDictionary dict2)
    {
        ArgumentNullException.ThrowIfNull(dict);
        ArgumentNullException.ThrowIfNull(dict2);
        
        ElementDictionary result = new(dict);
        foreach (KeyValuePair<Element, float> pair in dict2)
        {
            result[pair.Key] *= pair.Value;
        }
        return result;
    }

    
    public static ElementDictionary operator -(ElementDictionary dict, float scalar)
    {
        ArgumentNullException.ThrowIfNull(dict);

        ElementDictionary result = new(dict);
        foreach (KeyValuePair<Element, float> pair in result)
        {
            result[pair.Key] = pair.Value - scalar;
        }
        
        return result;
    }

    public static ElementDictionary operator -(ElementDictionary dict, ElementDictionary dict2)
    {
        ArgumentNullException.ThrowIfNull(dict);
        ArgumentNullException.ThrowIfNull(dict2);
        
        ElementDictionary result = new(dict);
        foreach (KeyValuePair<Element, float> pair in dict2)
        {
            result[pair.Key] -= pair.Value;
        }
        return result;
    }
    
    public static ElementDictionary operator /(ElementDictionary dict, float scalar)
    {
        ArgumentNullException.ThrowIfNull(dict);
        if (scalar == 0)
        {
            throw new DivideByZeroException();
        }

        ElementDictionary result = new(dict);
        foreach (KeyValuePair<Element, float> pair in result)
        {
            result[pair.Key] /= scalar;
        }
        
        return result;
    }

    public void operator += (float scalar)
    {
        foreach (KeyValuePair<Element, float> pair in this)
        {
            this[pair.Key] = pair.Value + scalar;
        }
    }

    public void operator -= (float scalar)
    {
        foreach (KeyValuePair<Element, float> pair in this)
        {
            this[pair.Key] = pair.Value - scalar;
        }
    }

    public void operator *= (float scalar)
    {
        foreach (KeyValuePair<Element, float> pair in this)
        {
            this[pair.Key] = pair.Value * scalar;
        }
    }

    public void operator /= (float scalar)
    {
        if (scalar == 0)
        {
            throw new DivideByZeroException();
        }
        foreach (KeyValuePair<Element, float> pair in this)
        {
            this[pair.Key] = pair.Value / scalar;
        }
    }

    public bool Equals(ElementDictionary? other)
    {
        if (other is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        if (Count != other.Count)
        {
            return false;
        }

        foreach (KeyValuePair<Element, float> pair in this)
        {
            if (!other.ContainsKey(pair.Key))
            {
                return false;
            }

            if (Math.Abs(other[pair.Key] - pair.Value) > 0.001f)
            {
                return false;
            }
        }

        return true;
    }

    public float Magnitude => Values.Sum();

    public ElementDictionary Packet(float packetSize)
    {
        if (packetSize > Magnitude)
        {
            return this;
        }

        float percent = packetSize / Magnitude;

        return this * percent;
    }

    public ElementDictionary Clone()
    {
        ElementDictionary newDict = new();
        foreach (KeyValuePair<Element, float> pair in this)
        {
            newDict[pair.Key] = pair.Value;
        }
        return newDict;
    }

    public override string ToString()
    {
        string[] elementStrings = this
            .Where(o => o.Value > 0.001f)
            .Select(o => $"{o.Key}: {MathF.Round(o.Value, 2)}")
            .ToArray();
        if (elementStrings.Length == 0)
        {
            return "{}";
        }

        return $"{{ {string.Join(", ", elementStrings)} }}";
    }
}