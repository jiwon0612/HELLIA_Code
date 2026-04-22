using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFragile
{
    public void AddWeight(int weight);
    public void ReduceWeight(int weight);
    public void Broke();
}
